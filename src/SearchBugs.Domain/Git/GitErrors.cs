using Shared.Errors;

namespace SearchBugs.Domain.Git;

public static class GitErrors
{
    public static Error InvalidCommitPath = new Error("Git.InvalidCommitPath", "Invalid path or commit.");

    public static Error FileNotFound = new Error("Git.FileNotFound", "File not found.");

    public static Error BranchNotFound = new Error("Git.BranchNotFound", "Branch not found.");

    public static Error CommitNotFound = new Error("Git.CommitNotFound", "Commit not found.");

    public static Error RepositoryNotFound = new Error("Git.RepositoryNotFound", "Repository not found.");

    public static Error RepositoryAlreadyExists = new Error("Git.RepositoryAlreadyExists", "Repository already exists.");

    public static Error CloneFailure(string message) => new Error("Git.CloneFailure", $"Failed to clone repository: {message}");

    public static Error PushFailure(string message) => new Error("Git.PushFailure", $"Failed to push: {message}");

    public static Error PullFailure(string message) => new Error("Git.PullFailure", $"Failed to pull: {message}");
}
