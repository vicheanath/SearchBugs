using Shared.Messaging;

namespace SearchBugs.Application.Git.MergePullRequest;

public record MergePullRequestCommand(string RepoUrl, Guid PullRequestId, string AuthorName, string AuthorEmail, Guid MergedByUserId) : ICommand;
