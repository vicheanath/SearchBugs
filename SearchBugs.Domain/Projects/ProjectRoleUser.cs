using SearchBugs.Domain.Users;

namespace SearchBugs.Domain.Projects;

public class ProjectRoleUser
{
    private ProjectRoleUser(ProjectId projectId, UserId userId, int roleId)
    {
        ProjectId = projectId;
        UserId = userId;
        RoleId = roleId;

    }

    public ProjectId ProjectId { get; private set; }
    public UserId UserId { get; private set; }
    public int RoleId { get; private set; }

    public static ProjectRoleUser Create(ProjectId projectId, UserId userId, int roleId) =>
        new(projectId, userId, roleId);
}
