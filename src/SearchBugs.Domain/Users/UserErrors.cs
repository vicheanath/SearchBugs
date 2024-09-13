using Shared.Errors;

namespace SearchBugs.Domain.Users;

public static class UserErrors
{

    public static Error EmailIsNotUnique => new ConflictError("User.EmailIsNotUnique", "The specified email is already in use.");

    public static Func<UserId, Error> NotFound => userId => new NotFoundError(
        "User.NotFound",
        $"The user with the identifier '{userId.Value}' was not found.");
    public static Func<string, Error> NotFoundByEmail => email => new NotFoundError(
        "User.NotFoundByEmail",
        $"The user with the email '{email}' was not found.");

    public static Error InvalidPassword => new("User.InvalidPassword", "The password is invalid.");
}
