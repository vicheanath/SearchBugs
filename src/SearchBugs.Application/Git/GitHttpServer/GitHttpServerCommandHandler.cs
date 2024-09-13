using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GitHttpServer;

internal sealed class GitHttpServerCommandHandler : ICommandHandler<GitHttpServerCommand>
{
    private readonly IGitHttpService _gitService;

    public GitHttpServerCommandHandler(IGitHttpService gitService) => _gitService = gitService;

    public async Task<Result> Handle(GitHttpServerCommand request, CancellationToken cancellationToken)
    {
        await _gitService.Handle(request.Name, cancellationToken);
        return Result.Success();
    }
}
