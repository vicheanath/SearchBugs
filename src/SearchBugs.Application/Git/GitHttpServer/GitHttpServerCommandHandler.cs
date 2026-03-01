using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SearchBugs.Domain.Git;
using Shared.Errors;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GitHttpServer;

internal sealed class GitHttpServerCommandHandler : ICommandHandler<GitHttpServerCommand>
{
    private const string GenericErrorMessage = "An error occurred while processing the request.";

    private readonly IGitHttpService _gitService;
    private readonly ILogger<GitHttpServerCommandHandler> _logger;

    public GitHttpServerCommandHandler(IGitHttpService gitService, ILogger<GitHttpServerCommandHandler> logger)
    {
        _gitService = gitService;
        _logger = logger;
    }

    public async Task<Result> Handle(GitHttpServerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command.HttpContext.Request.Method == "GET" &&
                command.Path.EndsWith("/info/refs"))
            {
                await _gitService.CreateRepository(command.Name, cancellationToken);
            }

            await _gitService.Handle(
                command.Name,
                command.Path,
                command.HttpContext,
                cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Git HTTP server error processing request for repository {RepositoryName}", command.Name);

            command.HttpContext.Response.StatusCode = ex switch
            {
                DirectoryNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            await command.HttpContext.Response.WriteAsync(GenericErrorMessage);
            return Result.Failure(Error.ConditionNotMet);
        }
    }
}
