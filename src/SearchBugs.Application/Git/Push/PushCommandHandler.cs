using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.Push;

internal sealed class PushCommandHandler : ICommandHandler<PushCommand>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public PushCommandHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result> Handle(PushCommand request, CancellationToken cancellationToken)
    {
        var result = _gitRepositoryService.Push(request.RepoUrl, request.BranchName, request.RemoteName);
        return Task.FromResult(result);
    }
}
