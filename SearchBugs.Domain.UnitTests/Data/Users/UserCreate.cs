using Bogus;
using SearchBugs.Domain.Users;

namespace SearchBugs.Domain.UnitTests.Data.Users;

internal sealed class UserCreate : TheoryData<User>
{
    public UserCreate()
    {
        var faker = new Faker();

        User userRegistration = User.Create(
            Name.Create(faker.Name.FirstName(), faker.Name.LastName()),
            Email.Create(faker.Internet.Email()),
            faker.Internet.Password());

        Add(userRegistration);
    }
}
