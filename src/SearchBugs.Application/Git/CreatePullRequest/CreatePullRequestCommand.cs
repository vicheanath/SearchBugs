using Shared.Messaging;

namespace SearchBugs.Application.Git.CreatePullRequest;

public record CreatePullRequestCommand(
    string RepoUrl,
    string SourceBranch,
    string TargetBranch,
    string Title,
    string Description,
    Guid CreatedByUserId) : ICommand<PullRequestDto>;
