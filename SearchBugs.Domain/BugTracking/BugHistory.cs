using SearchBugs.Domain.User;

namespace SearchBugs.Domain.BugTracking;
public class BugHistory
{
    public Guid Id { get; set; }
    public BugId BugId { get; set; }
    public UserId ChangedBy { get; set; }
    public string FieldChanged { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime ChangedAt { get; set; }

    private BugHistory()
    {

    }

    private BugHistory(Guid id, BugId bugId, UserId changedBy, string fieldChanged, string oldValue, string newValue, DateTime changedAt)
    {
        Id = id;
        BugId = bugId;
        ChangedBy = changedBy;
        FieldChanged = fieldChanged;
        OldValue = oldValue;
        NewValue = newValue;
        ChangedAt = changedAt;
    }

    public static BugHistory Create(BugId bugId, UserId changedBy, string fieldChanged, string oldValue, string newValue, DateTime changedAt)
    {
        var id = Guid.NewGuid();
        return new BugHistory(id, bugId, changedBy, fieldChanged, oldValue, newValue, changedAt);
    }
}
