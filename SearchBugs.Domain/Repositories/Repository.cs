using SearchBugs.Domain.Projects;
using Shared.Primitives;
using Shared.Time;

namespace SearchBugs.Domain.Repositories;

public class Repository : Entity<RepositoryId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
    public ProjectId ProjectId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    private Repository(RepositoryId id, string name, string description, string url, ProjectId projectId, DateTime createdAt) : base(id)
    {
        Name = name;
        Description = description;
        Url = url;
        ProjectId = projectId;
        CreatedAt = createdAt;
    }

    private Repository()
    {

    }

    public static Repository Create(string name, string description, string url, ProjectId projectId)
    {
        var id = new RepositoryId(Guid.NewGuid());
        return new Repository(id, name, description, url, projectId, SystemTime.UtcNow);
    }

}
