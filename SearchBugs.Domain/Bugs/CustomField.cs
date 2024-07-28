using SearchBugs.Domain.Projects;
using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;
public class CustomField : Entity<CustomFieldId>
{
    public CustomFieldId Id { get; set; }
    public string Name { get; set; }
    public string FieldType { get; set; }
    public ProjectId ProjectId { get; set; }


    private CustomField()
    {

    }

    private CustomField(CustomFieldId id, string name, string fieldType, ProjectId projectId) : base(id)
    {
        Name = name;
        FieldType = fieldType;
        ProjectId = projectId;
    }

    public static CustomField Create(string name, string fieldType, ProjectId projectId)
    {
        var id = new CustomFieldId(Guid.NewGuid());
        return new CustomField(id, name, fieldType, projectId);
    }

}
