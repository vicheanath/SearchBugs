using Shared.Primitives;

namespace SearchBugs.Domain.Notifications;

public record NotificationId(Guid Value) : IEntityId;