using Shared.Results;

namespace SearchBugs.Domain.Bugs;

public interface IBugRepository
{
    Task<Result<Bug>> GetByIdAsync(BugId id, CancellationToken cancellationToken);
    Task Add(Bug bug);

    Task<Result<BugStatus>> GetBugStatusByName(string name, CancellationToken cancellationToken);

    Task<Result<BugPriority>> GetBugPriorityByName(string name, CancellationToken cancellationToken);

}
