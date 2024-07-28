using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Users;
using Shared.Primitives;

namespace SearchBugs.Domain.Notifications;

public class Notification : Entity<NotificationId>
{
    public UserId UserId { get; }
    public string Type { get; }
    public string Message { get; }
    public BugId BugId { get; }
    public bool IsRead { get; private set; }
    public DateTime CreatedAt { get; }

    private Notification()
    {
    }

    private Notification(NotificationId id, UserId userId, string type, string message, BugId bugId, bool isRead, DateTime createdAt) : base(id)
    {
        UserId = userId;
        Type = type;
        Message = message;
        BugId = bugId;
        IsRead = isRead;
        CreatedAt = createdAt;
    }

    public static Notification Create(UserId userId, string type, string message, BugId bugId, bool isRead, DateTime createdAt)
    {
        var id = new NotificationId(Guid.NewGuid());
        return new Notification(id, userId, type, message, bugId, isRead, createdAt);
    }
}
