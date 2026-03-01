using Shared.Errors;

namespace SearchBugs.Domain.Git;

public static class PullRequestErrors
{
    public static Error NotFound => new("PullRequest.NotFound", "Pull request not found.");
    public static Error AlreadyMerged => new("PullRequest.AlreadyMerged", "Pull request is already merged.");
    public static Error BranchNotFound => new("PullRequest.BranchNotFound", "Source or target branch not found.");
}
