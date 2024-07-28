using Shared.Primitives;

namespace SearchBugs.Domain.Users;

public class User : Entity<UserId>
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    private readonly List<Role> _roles = new();


    private User(UserId id, Name name, Email email, string password)
        : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    private User()
    {
    }

    public static User Create(Name name, Email email, string password)
    {
        var id = new UserId(Guid.NewGuid());
        return new User(id, name, email, password);
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

}
