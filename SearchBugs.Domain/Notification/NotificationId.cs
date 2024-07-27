using Shared.Primitives;

namespace SearchBugs.Domain.Notification;

public record NotificationId(Guid Value) : IEntityId;