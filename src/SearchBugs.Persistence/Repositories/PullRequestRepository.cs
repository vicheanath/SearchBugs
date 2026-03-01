using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain.Git;
using SearchBugs.Persistence;

namespace SearchBugs.Persistence.Repositories;

internal sealed class PullRequestRepository : IPullRequestRepository
{
    private readonly ApplicationDbContext _context;

    public PullRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PullRequest?> GetByIdAsync(PullRequestId id, CancellationToken cancellationToken = default)
    {
        return await _context.PullRequests
            .FirstOrDefaultAsync(pr => pr.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PullRequest>> ListByRepoUrlAsync(string repoUrl, PullRequestStatus? status = null, CancellationToken cancellationToken = default)
    {
        var query = _context.PullRequests.Where(pr => pr.RepoUrl == repoUrl);
        if (status.HasValue)
            query = query.Where(pr => pr.Status == status.Value);
        return await query
            .OrderByDescending(pr => pr.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public void Add(PullRequest pullRequest)
    {
        _context.PullRequests.Add(pullRequest);
    }

    public void Update(PullRequest pullRequest)
    {
        _context.PullRequests.Update(pullRequest);
    }
}
