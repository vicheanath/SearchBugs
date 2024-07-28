using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class BugCustomField : Entity<BugCustomFieldId>
{
    public BugId BugId { get; set; }
    public CustomFieldId CustomFieldId { get; set; }
    public string Value { get; set; }

    private BugCustomField()
    {

    }

    private BugCustomField(BugCustomFieldId id, BugId bugId, CustomFieldId customFieldId, string value) : base(id)
    {
        BugId = bugId;
        CustomFieldId = customFieldId;
        Value = value;
    }

    public static BugCustomField Create(BugId bugId, CustomFieldId customFieldId, string value)
    {
        var id = new BugCustomFieldId(Guid.NewGuid());
        return new BugCustomField(id, bugId, customFieldId, value);
    }
}
