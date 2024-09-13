using Shared.Errors;

namespace SearchBugs.Application.Users;

internal static class UserValidationErrors
{
    internal static Error UserNotFound => new("User.UserNotFound", "The user was not found.");
    internal static Error FirstNameIsRequired => new("User.FirstNameIsRequired", "The user's first name is required.");
    internal static Error FirstNameMaxLength => new("User.FirstNameMaxLength", "The user's first name must not exceed 50 characters.");
    internal static Error LastNameIsRequired => new("User.LastNameIsRequired", "The user's last name is required.");
    internal static Error LastNameMaxLength => new("User.LastNameMaxLength", "The user's last name must not exceed 50 characters.");
}
