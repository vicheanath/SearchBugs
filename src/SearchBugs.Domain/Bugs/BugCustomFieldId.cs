using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public record BugCustomFieldId(Guid Value) : IEntityId;