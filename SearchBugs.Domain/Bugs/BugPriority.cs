using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class BugPriority : Enumeration<BugPriority>
{
    public static BugPriority Low = new(1, nameof(Low));
    public static BugPriority Medium = new(2, nameof(Medium));
    public static BugPriority High = new(3, nameof(High));

    private BugPriority(int id, string name) : base(id, name)
    {
    }
}
