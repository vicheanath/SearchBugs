using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public sealed record BugId(Guid Value) : IEntityId;