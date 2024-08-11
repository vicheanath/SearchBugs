using Shared.Errors;

namespace SearchBugs.Application.Git;

internal static class GitValidationErrors
{
    internal static Error NameIsRequired => new("Git.NameIsRequired", "The git's name is required.");
    internal static Error DescriptionIsRequired => new("Git.DescriptionIsRequired", "The git's description is required.");
    internal static Error UrlIsRequired => new("Git.UrlIsRequired", "The git's url is required.");

}
