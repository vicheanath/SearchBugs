using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public record AttachmentId(Guid Value) : IEntityId;