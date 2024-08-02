using Shared.Results;

namespace SearchBugs.Domain.Repositories;

public interface IGitRepository
{
    Task Add(Repository git);

    Task<Result<Repository>> GetByIdAsync(RepositoryId id, CancellationToken cancellationToken = default);
}
