using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public record TimeTracingId(Guid Value) : IEntityId;
