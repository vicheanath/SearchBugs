using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain.Bugs;
using Shared.Results;

namespace SearchBugs.Persistence.Repositories;

internal sealed class BugRepository : Repository<Bug, BugId>, IBugRepository
{
    public BugRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<Bug>> GetByIdAsync(BugId id, CancellationToken cancellationToken = default) =>
          Result.Create(await DbContext.Set<Bug>().FirstOrDefaultAsync(b => b.Id == id, cancellationToken));

    public async Task<Result<BugStatus>> GetBugStatusByName(string name, CancellationToken cancellationToken = default) =>
        Result.Create(await DbContext.Set<BugStatus>().FirstOrDefaultAsync(s => s.Name == name, cancellationToken));

    public async Task<Result<BugPriority>> GetBugPriorityByName(string name, CancellationToken cancellationToken) =>
        Result.Create(await DbContext.Set<BugPriority>().FirstOrDefaultAsync(p => p.Name == name, cancellationToken));
}
