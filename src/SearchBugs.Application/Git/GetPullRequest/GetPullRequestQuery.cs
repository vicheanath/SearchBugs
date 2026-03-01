using Shared.Messaging;

namespace SearchBugs.Application.Git.GetPullRequest;

public record GetPullRequestQuery(string RepoUrl, Guid PullRequestId) : IQuery<PullRequestDetailDto>;
