namespace SearchBugs.Domain.BugTracking;
//int bug_custom_field_id PK
//        int bug_id FK
//        int custom_field_id FK
//        string value
public class BugCustomField
{
    public Guid Id { get; set; }
    public Guid BugCustomFieldId { get; set; }
    public BugId BugId { get; set; }
    public Guid CustomFieldId { get; set; }
    public string Value { get; set; }

    private BugCustomField()
    {

    }

    private BugCustomField(Guid id, Guid bugCustomFieldId, BugId bugId, Guid customFieldId, string value)
    {
        Id = id;
        BugCustomFieldId = bugCustomFieldId;
        BugId = bugId;
        CustomFieldId = customFieldId;
        Value = value;
    }

    public static BugCustomField Create(Guid bugCustomFieldId, BugId bugId, Guid customFieldId, string value)
    {
        var id = Guid.NewGuid();
        return new BugCustomField(id, bugCustomFieldId, bugId, customFieldId, value);
    }

}
