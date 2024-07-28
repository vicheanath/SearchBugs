using Shared.Primitives;

namespace SearchBugs.Domain.Users;

public record UserId(Guid Value) : IEntityId;