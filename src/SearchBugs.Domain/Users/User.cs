using SearchBugs.Domain.Roles;
using SearchBugs.Domain.Users.Events;
using Shared.Primitives;
using Shared.Results;
using Shared.Time;

namespace SearchBugs.Domain.Users;

public class User : Entity<UserId>, IAuditable
{
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string Password { get; private set; } = string.Empty;
    public string? PasswordResetToken { get; private set; }
    public DateTime? PasswordResetTokenExpiry { get; private set; }
    public IReadOnlyCollection<Role> Roles => _roles.ToList().AsReadOnly();
    private readonly HashSet<Role> _roles = new();
    public Profile? Profile { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }


    private User(UserId id, Name name, Email email, string password, DateTime createdOnUtc) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        CreatedOnUtc = createdOnUtc;
    }

    private User() { }

    public static Result<User> Create(Name name, Email email, string password)
    {
        var id = new UserId(Guid.NewGuid());
        var user = new User(id, name, email, password, SystemTime.UtcNow);
        return Result.Create(user);
    }

    public void Update(Name name)
    {
        var userInformationChanged = Name != name;
        Name = name;
        if (userInformationChanged)
        {
            ModifiedOnUtc = SystemTime.UtcNow;
        }
        RaiseDomainEvent(new UserUpdatedEvent(Guid.NewGuid(), SystemTime.UtcNow, Id, Name, Email));
    }

    public void ChangePassword(string password)
    {
        Password = password;
    }

    public void SetPasswordResetToken(string token, DateTime expiry)
    {
        PasswordResetToken = token;
        PasswordResetTokenExpiry = expiry;
        ModifiedOnUtc = SystemTime.UtcNow;
    }

    public void ClearPasswordResetToken()
    {
        PasswordResetToken = null;
        PasswordResetTokenExpiry = null;
        ModifiedOnUtc = SystemTime.UtcNow;
    }

    public bool IsPasswordResetTokenValid(string token)
    {
        return !string.IsNullOrEmpty(PasswordResetToken) &&
               PasswordResetToken == token &&
               PasswordResetTokenExpiry.HasValue &&
               PasswordResetTokenExpiry.Value > SystemTime.UtcNow;
    }

    public void AddRole(Role role)
    {
        _roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        _roles.Remove(role);
    }

}
