using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class BugCustomField : ValueObject
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

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
        yield return BugCustomFieldId;
        yield return BugId;
        yield return CustomFieldId;
        yield return Value;
    }
}
