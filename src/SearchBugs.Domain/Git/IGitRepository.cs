using SearchBugs.Domain.Repositories;
using Shared.Results;

namespace SearchBugs.Domain.Git;

public interface IGitRepository : IRepository<Repository, RepositoryId>
{
    Task<Result<Repository>> GetByUrlAsync(string url, CancellationToken cancellationToken = default);
}
