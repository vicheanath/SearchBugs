using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class BugStatus : Enumeration<BugStatus>
{
    public static BugStatus Open = new(1, "Open");
    public static BugStatus InProgress = new(2, "In Progress");
    public static BugStatus Resolved = new(3, "Resolved");
    public static BugStatus Closed = new(4, "Closed");
    public BugStatus(int id, string name) : base(id, name)
    {
    }

}
