namespace SearchBugs.Application.Git.CreatePullRequest;

public record PullRequestDto(
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
    Guid? MergedBy);
