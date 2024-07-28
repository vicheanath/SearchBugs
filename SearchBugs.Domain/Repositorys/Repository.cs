using SearchBugs.Domain.Projects;
using Shared.Primitives;

namespace SearchBugs.Domain.Repositorys;

public class Repository : Entity<RepoId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
    public ProjectId ProjectId { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    private Repository(RepoId id, string name, string description, string url, ProjectId projectId)
        : base(id)
    {
        Name = name;
        Description = description;
        Url = url;
        ProjectId = projectId;
    }

    private Repository()
    {

    }

    public static Repository Create(string name, string description, string url, ProjectId projectId)
    {
        var id = new RepoId(Guid.NewGuid());
        return new Repository(id, name, description, url, projectId);
    }

}
