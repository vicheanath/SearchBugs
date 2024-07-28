using SearchBugs.Domain.Users;
using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;
public class BugHistory : Entity<HistoryId>
{
    public BugId BugId { get; set; }
    public UserId ChangedBy { get; set; }
    public string FieldChanged { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime ChangedAtUtc { get; set; }

    private BugHistory()
    {

    }

    private BugHistory(HistoryId id, BugId bugId, UserId changedBy, string fieldChanged, string oldValue, string newValue, DateTime changedAt) : base(id)
    {
        BugId = bugId;
        ChangedBy = changedBy;
        FieldChanged = fieldChanged;
        OldValue = oldValue;
        NewValue = newValue;
        ChangedAtUtc = changedAt;
    }

    public static BugHistory Create(BugId bugId, UserId changedBy, string fieldChanged, string oldValue, string newValue, DateTime changedAt)
    {
        var id = new HistoryId(Guid.NewGuid());
        return new BugHistory(id, bugId, changedBy, fieldChanged, oldValue, newValue, changedAt);
    }
}
