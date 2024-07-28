using Shared.Results;

namespace SearchBugs.Domain.Bugs;

public interface IBugRepository
{
    Task<Result<Bug>> GetByIdAsync(BugId id, CancellationToken cancellationToken);
    void Add(Bug bug);
}
