using Shared.Results;

namespace SearchBugs.Domain.Repositories;

public interface IGitRepository : IRepository<Repository, RepositoryId>
{
    Task<Result<Repository>> GetByUrlAsync(string url, CancellationToken cancellationToken = default);
}
