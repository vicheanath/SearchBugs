using SearchBugs.Domain.Roles;
using Shared.Primitives;
using Shared.Results;
using Shared.Time;

namespace SearchBugs.Domain.Users;

public class User : Entity<UserId>, IAuditable
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public IReadOnlyCollection<Role> Roles => _roles.ToList().AsReadOnly();
    private readonly HashSet<Role> _roles = new();
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
        return user;
    }

    public void Update(Name name, Email email)
    {
        Name = name;
        Email = email;
    }

    public void ChangePassword(string password)
    {
        Password = password;
    }

    public void AddRole(Role role)
    {
        _roles.Add(role);
    }

}
