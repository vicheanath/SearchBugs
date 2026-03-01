using Shared.Results;

namespace SearchBugs.Domain.Git;

public interface IPullRequestRepository
{
    Task<PullRequest?> GetByIdAsync(PullRequestId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PullRequest>> ListByRepoUrlAsync(string repoUrl, PullRequestStatus? status = null, CancellationToken cancellationToken = default);
    void Add(PullRequest pullRequest);
    void Update(PullRequest pullRequest);
}
