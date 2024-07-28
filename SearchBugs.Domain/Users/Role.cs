using Shared.Primitives;

namespace SearchBugs.Domain.Users;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Admin = new(1, "Admin");
    public static readonly Role ProjectManager = new(2, "Project Manager");
    public static readonly Role Developer = new(3, "Developer");
    public static readonly Role Reporter = new(4, "Reporter");
    public static readonly Role Guest = new(5, "Guest");

    public Role(int id, string name)
        : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; }

    public ICollection<User> Users { get; set; }
}
