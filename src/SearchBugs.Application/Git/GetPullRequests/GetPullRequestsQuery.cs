using Shared.Messaging;

namespace SearchBugs.Application.Git.GetPullRequests;

public record GetPullRequestsQuery(string RepoUrl, string? Status = null) : IQuery<IEnumerable<PullRequestListItemDto>>;
