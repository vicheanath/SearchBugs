namespace SearchBugs.Domain.Users;

public sealed class UserRole
{
    public UserRole(UserId userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    private UserRole()
    {
    }
    public UserId UserId { get; private set; }

    public int RoleId { get; private set; }
}

