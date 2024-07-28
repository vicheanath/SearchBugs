using SearchBugs.Domain.Bugs;
using Shared.Results;

namespace SearchBugs.Persistence.Repositories;

internal sealed class BugRepository : Repository<Bug, BugId>, IBugRepository
{
    public BugRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Result<Bug>> GetByIdAsync(BugId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
