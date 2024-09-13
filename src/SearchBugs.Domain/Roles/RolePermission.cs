namespace SearchBugs.Domain.Roles;

public sealed class RolePermission
{
    public RolePermission(Role role, Permission permission)
    {
        RoleId = role.Id;
        PermissionId = permission.Id;
    }

    private RolePermission()
    {
    }

    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }
}
