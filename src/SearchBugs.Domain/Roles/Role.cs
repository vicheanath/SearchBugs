using SearchBugs.Domain.Users;
using Shared.Primitives;

namespace SearchBugs.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Admin = new(1, "Admin");
    public static readonly Role ProjectManager = new(2, "Project Manager");
    public static readonly Role Developer = new(3, "Developer");
    public static readonly Role Reporter = new(4, "Reporter");
    public static readonly Role Guest = new(5, "Guest");

    public IReadOnlyCollection<User> Users { get; } = new List<User>();


    public Role(int id, string name)
        : base(id, name)
    {
    }

    private Role()
    {
    }
}
