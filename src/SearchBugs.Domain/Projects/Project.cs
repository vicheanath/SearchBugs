using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Users;
using Shared.Primitives;
using Shared.Results;
using Shared.Time;

namespace SearchBugs.Domain.Projects;

public class Project : Entity<ProjectId>, IAuditable
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();
    private readonly List<User> _users = new();

    public IReadOnlyCollection<Bug> Bugs => _bugs.AsReadOnly();
    private readonly List<Bug> _bugs = new();

    public IReadOnlyCollection<CustomField> CustomsFields => _customFields.AsReadOnly();

    private List<CustomField> _customFields = new();

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }



    private Project(ProjectId id, string name, string description, DateTime createdOnUtc) : base(id)

    {
        Name = name;
        Description = description;
        CreatedOnUtc = createdOnUtc;
    }

    private Project()
    {

    }

    public static Result<Project> Create(string name, string description)
    {
        var id = new ProjectId(Guid.NewGuid());
        return new Project(id, name, description, SystemTime.UtcNow);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void RemoveUser(User user)
    {
        _users.Remove(user);
    }

}
