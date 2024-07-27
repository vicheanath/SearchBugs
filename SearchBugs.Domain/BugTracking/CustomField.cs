namespace SearchBugs.Domain.BugTracking;
public class CustomField
{
    public Guid CustomFieldId { get; set; }
    public string Name { get; set; }
    public string FieldType { get; set; }
    public Guid ProjectId { get; set; }


    private CustomField()
    {

    }

    private CustomField(Guid customFieldId, string name, string fieldType, Guid projectId)
    {
        CustomFieldId = customFieldId;
        Name = name;
        FieldType = fieldType;
        ProjectId = projectId;
    }

    public static CustomField Create(string name, string fieldType, Guid projectId)
    {
        var id = Guid.NewGuid();
        return new CustomField(id, name, fieldType, projectId);
    }
}
