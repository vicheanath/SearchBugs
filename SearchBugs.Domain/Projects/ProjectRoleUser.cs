using SearchBugs.Domain.Users;

namespace SearchBugs.Domain.Projects;

public class ProjectRoleUser
{
    private ProjectRoleUser(ProjectId projectId, UserId userId, ProjectRole role)
    {
        ProjectId = projectId;
        UserId = userId;
        Role = role;
    }

    public ProjectId ProjectId { get; private set; }
    public UserId UserId { get; private set; }
    public int RoleId { get; private set; }
    public ProjectRole Role { get; private set; }

    public static ProjectRoleUser Create(ProjectId projectId, UserId userId, ProjectRole role) =>
        new(projectId, userId, role);
}
