using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetCommitDiff;

internal sealed class GetCommitDiffQueryHandler : IQueryHandler<GetCommitDiffQuery, IEnumerable<CommitDiffResult>>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public GetCommitDiffQueryHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result<IEnumerable<CommitDiffResult>>> Handle(GetCommitDiffQuery request, CancellationToken cancellationToken)
    {
        var commitDiffResult = _gitRepositoryService.GetCommitDiff(request.Url, request.CommitSha);
        if (commitDiffResult.IsFailure)
        {
            return Task.FromResult(Result.Failure<IEnumerable<CommitDiffResult>>(commitDiffResult.Error));
        }

        var commitDiffs = commitDiffResult.Value.Select(fileDiff => new CommitDiffResult
        (
            FilePath: fileDiff.FilePath,
            OldPath: fileDiff.OldPath,
            Status: fileDiff.Status,
            Patch: fileDiff.Patch
        ));

        return Task.FromResult(Result.Success(commitDiffs));
    }
}
