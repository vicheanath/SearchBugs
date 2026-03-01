using System.Diagnostics;
using System.IO.Pipelines;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SearchBugs.Domain.Git;
using SearchBugs.Infrastructure.Options;

namespace SearchBugs.Infrastructure.Services;

internal class GitHttpService : IGitHttpService
{
    private readonly GitOptions _gitOptions;

    public GitHttpService(IOptions<GitOptions> gitOptions)
    {
        _gitOptions = gitOptions.Value;
    }

    public Task DeleteRepository(string repositoryName, CancellationToken cancellationToken = default)
    {
        var gitPath = Path.Combine(_gitOptions.BasePath, repositoryName);
        if (Directory.Exists(gitPath))
        {
            Directory.Delete(gitPath, true);
        }
        return Task.CompletedTask;
    }

    public async Task CreateRepository(string repositoryName, CancellationToken cancellationToken = default)
    {
        var gitPath = Path.Combine(_gitOptions.BasePath, repositoryName);
        if (!Directory.Exists(gitPath))
        {
            Directory.CreateDirectory(gitPath);
            using var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "init --bare",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = gitPath
            };
            process.Start();
            await process.WaitForExitAsync(cancellationToken);
            if (process.ExitCode != 0)
            {
                var error = await process.StandardError.ReadToEndAsync(cancellationToken);
                throw new InvalidOperationException($"git init failed: {error}");
            }
        }
    }

    public async Task Handle(string repositoryName, string path, HttpContext httpContext, CancellationToken cancellationToken = default)
    {
        var gitPath = Path.Combine(_gitOptions.BasePath, $"{repositoryName}");
        ValidateRepository(gitPath);

        using var process = new Process();
        ConfigureProcess(repositoryName, process, gitPath, httpContext, path);

        process.Start();

        await HandleRequestPayload(httpContext, process, cancellationToken);
        await HandleResponse(httpContext, process, cancellationToken);
    }

    private void ValidateRepository(string gitPath)
    {
        if (!Directory.Exists(Path.Combine(gitPath, "objects")))
        {
            throw new DirectoryNotFoundException("Not a valid Git repository");
        }
    }

    private void ConfigureProcess(string repositoryName, Process process, string gitPath, HttpContext context, string path)
    {
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = "http-backend",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = gitPath,
            Environment =
            {
                ["GIT_HTTP_EXPORT_ALL"] = "1",
                ["GIT_PROJECT_ROOT"] = _gitOptions.BasePath,
                ["PATH_INFO"] = $"/{repositoryName}.git/{path.TrimStart('/')}",
                ["REQUEST_METHOD"] = context.Request.Method,
                ["QUERY_STRING"] = context.Request.QueryString.Value?.TrimStart('?') ?? "",
                ["CONTENT_TYPE"] = context.Request.ContentType ?? GetDefaultContentType(path),
                ["CONTENT_LENGTH"] = GetContentLength(context.Request),
                ["REMOTE_ADDR"] = context.Connection.RemoteIpAddress?.ToString(),
                ["GIT_COMMITTER_NAME"] = context.User.Identity?.Name ?? "git-user"
            }
        };
    }

    private string GetDefaultContentType(string path)
    {
        return path.Contains("git-upload-pack") ?
            "application/x-git-upload-pack-request" :
            "application/x-git-receive-pack-request";
    }

    private string GetContentLength(HttpRequest request)
    {
        return request.ContentLength.HasValue ?
            request.ContentLength.Value.ToString() :
            "0";
    }

    private async Task HandleRequestPayload(HttpContext context, Process process, CancellationToken cancellationToken)
    {
        if (context.Request.ContentLength > 0)
        {
            await context.Request.Body.CopyToAsync(process.StandardInput.BaseStream, cancellationToken);
        }
        process.StandardInput.Close();
    }

    private async Task HandleResponse(HttpContext context, Process process, CancellationToken cancellationToken)
    {
        var stdout = process.StandardOutput.BaseStream;
        await ReadCgiHeadersAsync(stdout, context.Response, cancellationToken);

        var outputPipe = PipeWriter.Create(context.Response.Body);
        var inputPipe = PipeReader.Create(stdout);

        while (true)
        {
            var readResult = await inputPipe.ReadAsync(cancellationToken);

            foreach (var segment in readResult.Buffer)
            {
                await outputPipe.WriteAsync(segment, cancellationToken);
            }

            inputPipe.AdvanceTo(readResult.Buffer.End);

            if (readResult.IsCompleted)
            {
                break;
            }
        }

        await outputPipe.FlushAsync(cancellationToken);

        var stderrTask = process.StandardError.ReadToEndAsync(cancellationToken);
        await process.WaitForExitAsync(cancellationToken);
        var stderr = await stderrTask;

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException($"Git error: {stderr}");
        }
    }

    /// <summary>
    /// Reads CGI-style headers from git-http-backend stdout (until \r\n\r\n),
    /// applies Status and Content-Type to the response. The body is left on the stream for the caller to read.
    /// </summary>
    private static async Task ReadCgiHeadersAsync(
        Stream stdout,
        HttpResponse response,
        CancellationToken cancellationToken)
    {
        var buffer = new List<byte>();
        byte[] end = [ (byte)'\r', (byte)'\n', (byte)'\r', (byte)'\n' ];
        var chunk = new byte[1];

        while (true)
        {
            var read = await stdout.ReadAsync(chunk, cancellationToken);
            if (read == 0)
                break;
            buffer.Add(chunk[0]);
            if (buffer.Count >= 4 &&
                buffer[^4] == end[0] && buffer[^3] == end[1] && buffer[^2] == end[2] && buffer[^1] == end[3])
                break;
        }

        if (buffer.Count < 4)
            return;

        var headerBytes = buffer.Take(buffer.Count - 4).ToArray();
        var headerText = Encoding.ASCII.GetString(headerBytes);
        var statusCode = StatusCodes.Status200OK;
        var contentType = (string?)null;

        foreach (var line in headerText.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
        {
            if (line.StartsWith("Status:", StringComparison.OrdinalIgnoreCase))
            {
                var rest = line.AsSpan().Slice(7).Trim();
                var space = rest.IndexOf(' ');
                if (space >= 0)
                    int.TryParse(rest.Slice(0, space).ToString(), out statusCode);
            }
            else if (line.StartsWith("Content-type:", StringComparison.OrdinalIgnoreCase))
            {
                contentType = line.AsSpan().Slice(12).Trim().ToString();
            }
        }

        response.StatusCode = statusCode;
        if (!string.IsNullOrEmpty(contentType))
            response.ContentType = contentType;
    }
}



