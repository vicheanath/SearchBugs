using Shared.Results;

namespace SearchBugs.Domain.Bugs;

public interface IBugRepository : IRepository<Bug, BugId>
{
    Task<Result<BugStatus>> GetBugStatusByName(string name, CancellationToken cancellationToken);

    Task<Result<BugPriority>> GetBugPriorityByName(string name, CancellationToken cancellationToken);
}
