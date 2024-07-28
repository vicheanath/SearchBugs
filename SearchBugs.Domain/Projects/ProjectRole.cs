using Shared.Primitives;

namespace SearchBugs.Domain.Projects;

public class ProjectRole : Enumeration<ProjectRole>
{
    public static ProjectRole Owner = new(0, nameof(Owner));
    public static ProjectRole Admin = new(1, nameof(Admin));
    public static ProjectRole Developer = new(2, nameof(Developer));
    public static ProjectRole Tester = new(3, nameof(Tester));

    private ProjectRole(int id, string name) : base(id, name)
    {
    }
}
