using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public record CustomFieldId(Guid Value) : IEntityId;