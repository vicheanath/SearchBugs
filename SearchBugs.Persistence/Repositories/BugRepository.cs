using SearchBugs.Domain.BugTracking;

namespace SearchBugs.Persistence.Repositories;

internal sealed class BugRepository : Repository<Bug, BugId>, IBugRepository
{
    public BugRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
