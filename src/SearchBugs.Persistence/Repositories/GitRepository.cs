using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain.Repositories;
using Shared.Results;

namespace SearchBugs.Persistence.Repositories;

internal sealed class GitRepository : Repository<Repository, RepositoryId>, IGitRepository
{
    public GitRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<Repository>> GetByIdAsync(RepositoryId id, CancellationToken cancellationToken = default) =>
        Result.Create(await _context.Set<Repository>().FirstOrDefaultAsync(r => r.Id == id, cancellationToken));

    public async Task<Result<Repository>> GetByUrlAsync(string url, CancellationToken cancellationToken = default) =>
        Result.Create(await _context.Set<Repository>().FirstOrDefaultAsync(r => r.Url == url, cancellationToken));
}

