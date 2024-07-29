using Shared.Errors;

namespace SearchBugs.Domain.Users;

/// <summary>
/// Contains the users errors.
/// </summary>
public static class UserErrors
{
    /// <summary>
    /// Gets the email is not unique error.
    /// </summary>
    public static Error EmailIsNotUnique => new ConflictError("User.EmailIsNotUnique", "The specified email is already in use.");

    /// <summary>
    /// Gets the not found error.
    /// </summary>
    public static Func<UserId, Error> NotFound => userId => new NotFoundError(
        "User.NotFound",
        $"The user with the identifier '{userId.Value}' was not found.");

    /// <summary>
    /// Gets the not found by identity error.
    /// </summary>
    public static Func<string, Error> NotFoundByEmail => email => new NotFoundError(
        "User.NotFoundByEmail",
        $"The user with the email '{email}' was not found.");

    public static Error InvalidPassword => new("User.InvalidPassword", "The password is invalid.");
}
