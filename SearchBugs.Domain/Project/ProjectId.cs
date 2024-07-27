using Shared.Primitives;

namespace SearchBugs.Domain.Project;

public record ProjectId(Guid Value) : IEntityId;