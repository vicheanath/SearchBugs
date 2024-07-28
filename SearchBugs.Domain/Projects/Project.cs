using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Users;
using Shared.Primitives;

namespace SearchBugs.Domain.Projects;

public class Project : Entity<ProjectId>, IAuditable
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<Bug> Bugs { get; } = new();
    public List<UserId> Users { get; } = new();

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public IReadOnlyCollection<CustomField> CustomsFields => _customFields.AsReadOnly();

    private List<CustomField> _customFields = new();

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

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddUser(UserId userId)
    {
        Users.Add(userId);
    }

    public void RemoveUser(UserId userId)
    {
        Users.Remove(userId);
    }

}
