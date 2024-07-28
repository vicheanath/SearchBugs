using Shared.Primitives;

namespace SearchBugs.Domain.Projects;

public record ProjectId(Guid Value) : IEntityId;