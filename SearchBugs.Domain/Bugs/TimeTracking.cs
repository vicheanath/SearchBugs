using SearchBugs.Domain.Users;
using Shared.Primitives;
using Shared.Time;

namespace SearchBugs.Domain.Bugs;

public class TimeTracking : Entity<TimeTracingId>
{
    public BugId BugId { get; set; }
    public UserId UserId { get; set; }
    public DateTime? TimeSpent { get; set; }
    public DateTime LoggedAt { get; set; }
    public TimeTracking(TimeTracingId id, BugId bugId, UserId userId, DateTime timeSpent, DateTime loggedAt) : base(id)
    {
        BugId = bugId;
        UserId = userId;
        TimeSpent = timeSpent;
        LoggedAt = loggedAt;
    }

    public TimeTracking()
    {
    }

    public static TimeTracking Create(BugId bugId, UserId userId, DateTime timeSpent)
    {
        var id = new TimeTracingId(Guid.NewGuid());
        return new TimeTracking(id, bugId, userId, timeSpent, SystemTime.UtcNow);
    }

}
