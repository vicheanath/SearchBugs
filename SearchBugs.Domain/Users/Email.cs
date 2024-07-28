using Shared.Primitives;

namespace SearchBugs.Domain.Users;

public class Email : ValueObject
{
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be empty");
        }

        return new Email(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string value) => Create(value);
}