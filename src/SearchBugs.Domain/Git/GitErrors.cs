using Shared.Errors;

namespace SearchBugs.Domain.Git;

public static class GitErrors
{
    public static Error InvalidCommitPath = new Error("Git.InvalidCommitPath", "Invalid path or commit.");

    public static Error FileNotFound = new Error("Git.FileNotFound", "File not found.");

    public static Error BranchNotFound = new Error("Git.BranchNotFound", "Branch not found.");

    public static Error CommitNotFound = new Error("Git.CommitNotFound", "Commit not found.");
}
