using Shared.Errors;

namespace SearchBugs.Application.Authentications;

internal static class AuthValidationErrors
{
    internal static Error EmailIsRequired => new("User.EmailIsRequired", "The user's email is required.");
    internal static Error PasswordIsRequired => new("User.PasswordIsRequired", "The user's password is required.");

    internal static Error EmailAlreadyExists => new("User.EmailAlreadyExists", "The user's email already exists.");

    internal static Func<string, Error> PasswordNotMatchRequirements = (requirements) =>
        new Error("User.PasswordNotMatchRequirements", $"{requirements}");


    internal static Error InvalidEmail => new("User.InvalidEmail", "The user's email is invalid.");

    internal static Error InvalidPassword => new("User.InvalidPassword", "The user's password is invalid.");

    internal static Error UserNotFound => new("User.UserNotFound", "The user was not found.");

}
