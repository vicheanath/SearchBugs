using Shared.Primitives;

namespace SearchBugs.Domain.Projects;

public class Role : Enumeration<Role>
{
    public static Role Owner = new(0, nameof(Owner));
    public static Role Admin = new(1, nameof(Admin));
    public static Role Developer = new(2, nameof(Developer));
    public static Role Tester = new(3, nameof(Tester));

    private Role(int id, string name) : base(id, name)
    {
    }
}
