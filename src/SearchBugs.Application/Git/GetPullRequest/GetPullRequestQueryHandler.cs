using SearchBugs.Application.Git.GetCommitDiff;
using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetPullRequest;

internal sealed class GetPullRequestQueryHandler : IQueryHandler<GetPullRequestQuery, PullRequestDetailDto>
{
    private readonly IPullRequestRepository _pullRequestRepository;
    private readonly IGitRepositoryService _gitRepositoryService;

    public GetPullRequestQueryHandler(
        IPullRequestRepository pullRequestRepository,
        IGitRepositoryService gitRepositoryService)
    {
        _pullRequestRepository = pullRequestRepository;
        _gitRepositoryService = gitRepositoryService;
    }

    public async Task<Result<PullRequestDetailDto>> Handle(GetPullRequestQuery request, CancellationToken cancellationToken)
    {
        var pr = await _pullRequestRepository.GetByIdAsync(new PullRequestId(request.PullRequestId), cancellationToken);
        if (pr == null || pr.RepoUrl != request.RepoUrl)
            return Result.Failure<PullRequestDetailDto>(PullRequestErrors.NotFound);

        var targetCommits = _gitRepositoryService.GetCommits(request.RepoUrl, pr.TargetBranch, 0, 1);
        var sourceCommits = _gitRepositoryService.GetCommits(request.RepoUrl, pr.SourceBranch, 0, 1);
        if (targetCommits.IsFailure || sourceCommits.IsFailure)
            return Result.Failure<PullRequestDetailDto>(PullRequestErrors.BranchNotFound);
        var baseSha = targetCommits.Value.First().Sha;
        var compareSha = sourceCommits.Value.First().Sha;

        var diffResult = _gitRepositoryService.CompareCommits(request.RepoUrl, baseSha, compareSha);
        var diffs = diffResult.IsSuccess
            ? diffResult.Value.Select(f => new CommitDiffResult(f.FilePath, f.OldPath, f.Status, f.Patch))
            : Array.Empty<CommitDiffResult>();

        var dto = new PullRequestDetailDto(
            pr.Id.Value,
            pr.RepoUrl,
            pr.SourceBranch,
            pr.TargetBranch,
            pr.Title,
            pr.Description,
            pr.Status.ToString(),
            pr.CreatedBy.Value,
            pr.CreatedAtUtc,
            pr.MergedAtUtc,
            pr.MergedBy?.Value,
            diffs);
        return Result.Success(dto);
    }
}
