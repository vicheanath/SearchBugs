using Shared.Primitives;

namespace SearchBugs.Domain.User;

public record UserId(Guid Value) : IEntityId;