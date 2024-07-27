using Shared.Primitives;

namespace SearchBugs.Domain.Project;

public class Project : Entity<ProjectId>
{
    public string Name { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; } = DateTime.UtcNow;

    private Project(ProjectId id, string name, string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    private Project()
    {

    }

    public static Project Create(string name, string description)
    {
        var id = new ProjectId(Guid.NewGuid());
        return new Project(id, name, description);
    }

}
