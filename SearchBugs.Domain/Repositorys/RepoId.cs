using Shared.Primitives;

namespace SearchBugs.Domain.Repositorys;

public record RepoId(Guid Value) : IEntityId;