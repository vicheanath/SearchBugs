using Shared.Primitives;

namespace SearchBugs.Domain.Repositories;

public record RepositoryId(Guid Value) : IEntityId;