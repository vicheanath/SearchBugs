using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.MergeBranches;

internal sealed class MergeBranchesCommandHandler : ICommandHandler<MergeBranchesCommand>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public MergeBranchesCommandHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result> Handle(MergeBranchesCommand request, CancellationToken cancellationToken)
    {
        var result = _gitRepositoryService.MergeBranches(
            request.RepoUrl,
            request.SourceBranch,
            request.TargetBranch,
            request.AuthorName,
            request.AuthorEmail);
        return Task.FromResult(result.IsSuccess ? Result.Success() : Result.Failure(result.Error));
    }
}
