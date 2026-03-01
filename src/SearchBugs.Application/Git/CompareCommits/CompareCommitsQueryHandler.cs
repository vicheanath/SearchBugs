using SearchBugs.Application.Git.GetCommitDiff;
using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.CompareCommits;

internal sealed class CompareCommitsQueryHandler : IQueryHandler<CompareCommitsQuery, IEnumerable<CommitDiffResult>>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public CompareCommitsQueryHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result<IEnumerable<CommitDiffResult>>> Handle(CompareCommitsQuery request, CancellationToken cancellationToken)
    {
        var result = _gitRepositoryService.CompareCommits(request.RepoUrl, request.BaseCommitSha, request.CompareCommitSha);
        if (result.IsFailure)
            return Task.FromResult(Result.Failure<IEnumerable<CommitDiffResult>>(result.Error));
        var dtos = result.Value.Select(f => new CommitDiffResult(f.FilePath, f.OldPath, f.Status, f.Patch));
        return Task.FromResult(Result.Success(dtos.AsEnumerable()));
    }
}
