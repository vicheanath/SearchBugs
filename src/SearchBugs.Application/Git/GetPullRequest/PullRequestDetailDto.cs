using SearchBugs.Application.Git.GetCommitDiff;

namespace SearchBugs.Application.Git.GetPullRequest;

public record PullRequestDetailDto(
    Guid Id,
    string RepoUrl,
    string SourceBranch,
    string TargetBranch,
    string Title,
    string Description,
    string Status,
    Guid CreatedBy,
    DateTime CreatedAtUtc,
    DateTime? MergedAtUtc,
    Guid? MergedBy,
    IEnumerable<CommitDiffResult> Diff);
