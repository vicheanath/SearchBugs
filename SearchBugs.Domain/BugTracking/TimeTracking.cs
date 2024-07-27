using SearchBugs.Domain.User;

namespace SearchBugs.Domain.BugTracking;

public class TimeTracking
{
    public TimeTracking()
    {
    }

    public Guid Id { get; set; }

    public BugId BugId { get; set; }

    public UserId UserId { get; set; }

    public DateTime TimeSpent { get; set; }
    public DateTime LoggedAt { get; set; }


    public TimeTracking(Guid id, BugId bugId, UserId userId, DateTime timeSpent, DateTime loggedAt)
    {
        Id = id;
        BugId = bugId;
        UserId = userId;
        TimeSpent = timeSpent;
        LoggedAt = loggedAt;
    }

    public static TimeTracking Create(BugId bugId, UserId userId, DateTime timeSpent, DateTime loggedAt)
    {
        var id = Guid.NewGuid();
        return new TimeTracking(id, bugId, userId, timeSpent, loggedAt);
    }



}
