using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public record CommentId(Guid Value) : IEntityId;
