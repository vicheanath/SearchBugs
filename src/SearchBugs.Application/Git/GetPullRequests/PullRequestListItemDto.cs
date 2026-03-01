namespace SearchBugs.Application.Git.GetPullRequests;

public record PullRequestListItemDto(
    Guid Id,
    string Title,
    string SourceBranch,
    string TargetBranch,
    string Status,
    Guid CreatedBy,
    DateTime CreatedAtUtc);
