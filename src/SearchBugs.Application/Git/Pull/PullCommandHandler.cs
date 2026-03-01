using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.Pull;

internal sealed class PullCommandHandler : ICommandHandler<PullCommand>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public PullCommandHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result> Handle(PullCommand request, CancellationToken cancellationToken)
    {
        var result = _gitRepositoryService.Pull(
            request.RepoUrl,
            request.BranchName,
            request.AuthorName,
            request.AuthorEmail,
            request.RemoteName);
        return Task.FromResult(result);
    }
}
