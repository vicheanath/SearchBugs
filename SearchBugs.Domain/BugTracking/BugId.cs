using Shared.Primitives;

namespace SearchBugs.Domain.BugTracking;

public sealed record BugId(Guid Value) : IEntityId;