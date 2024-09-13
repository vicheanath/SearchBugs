using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class BugSeverity : Enumeration<BugSeverity>
{
    public static BugSeverity Low = new(1, nameof(Low));
    public static BugSeverity Medium = new(2, nameof(Medium));
    public static BugSeverity High = new(3, nameof(High));

    private BugSeverity(int id, string name) : base(id, name)
    {
    }

    private BugSeverity()
    {
    }
}
