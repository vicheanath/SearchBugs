using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public record HistoryId(Guid Value) : IEntityId;
