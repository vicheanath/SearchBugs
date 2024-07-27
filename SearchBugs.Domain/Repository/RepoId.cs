using Shared.Primitives;

namespace SearchBugs.Domain.Repository;

public record RepoId(Guid Value) : IEntityId;