using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SearchBugs.Domain.Git;
using SearchBugs.Infrastructure.Options;
using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Text;
namespace SearchBugs.Infrastructure.Services;

internal class GitHttpService : IGitHttpService
{
    private readonly GitOptions _gitOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpContext => _httpContextAccessor.HttpContext!;

    public GitHttpService(IOptions<GitOptions> gitOptions, IHttpContextAccessor httpContextAccessor)
    {
        _gitOptions = gitOptions.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Handle(string repositoryName, CancellationToken cancellationToken = default)
    {
        var gitPath = Path.Combine(_gitOptions.BasePath, repositoryName);

        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = "http-backend --stateless-rpc --advertise-refs",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = gitPath,
            EnvironmentVariables =
            {
                { "GIT_HTTP_EXPORT_ALL", "1" },
                { "HTTP_GIT_PROTOCOL", _httpContext.Request.Headers["Git-Protocol"] },
                { "REQUEST_METHOD", _httpContext.Request.Method },
                { "GIT_PROJECT_ROOT", gitPath },
                { "PATH_INFO", $"/{_httpContext.Request.RouteValues["path"]}" },
                { "QUERY_STRING", _httpContext.Request.QueryString.ToUriComponent().TrimStart('?') },
                { "CONTENT_TYPE", _httpContext.Request.ContentType },
                { "CONTENT_LENGTH", _httpContext.Request.ContentLength?.ToString() },
                { "HTTP_CONTENT_ENCODING", _httpContext.Request.Headers.ContentEncoding },
                { "REMOTE_USER", _httpContext.User.Identity?.Name },
                { "REMOTE_ADDR", _httpContext.Connection.RemoteIpAddress?.ToString() },
                { "GIT_COMMITTER_NAME", _httpContext.User.Identity?.Name },
                { "GIT_COMMITTER_EMAIL", "TODO: some email" },
            },
        };
        process.Start();

        var pipeWriter = PipeWriter.Create(process.StandardInput.BaseStream);
        await _httpContext.Request.BodyReader.CopyToAsync(pipeWriter, cancellationToken);

        var pipeReader = PipeReader.Create(process.StandardOutput.BaseStream);
        await ReadResponse(pipeReader, cancellationToken);

        await pipeReader.CopyToAsync(_httpContext.Response.BodyWriter, cancellationToken);
        await pipeReader.CompleteAsync();
    }

    private async Task ReadResponse(PipeReader pipeReader, CancellationToken cancellationToken)
    {
        while (true)
        {
            var result = await pipeReader.ReadAsync(cancellationToken);
            var buffer = result.Buffer;
            var (position, isFinished) = ReadHeaders(_httpContext, buffer);
            pipeReader.AdvanceTo(position, buffer.End);

            if (result.IsCompleted || isFinished)
                break;
        }
    }

    private static (SequencePosition Position, bool IsFinished) ReadHeaders(
        HttpContext httpContext,
        in ReadOnlySequence<byte> sequence)
    {
        var reader = new SequenceReader<byte>(sequence);
        while (!reader.End)
        {
            if (!reader.TryReadTo(out ReadOnlySpan<byte> line, (byte)'\n'))
                break;

            if (line.Length == 1)
                return (reader.Position, true);

            var colon = line.IndexOf((byte)':');
            if (colon == -1)
                break;

            var headerName = Encoding.UTF8.GetString(line[..colon]);
            var headerValue = Encoding.UTF8.GetString(line[(colon + 1)..]).Trim();
            httpContext.Response.Headers[headerName] = headerValue;
        }

        return (reader.Position, false);
    }

}



