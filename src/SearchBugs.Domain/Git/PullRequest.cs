using SearchBugs.Domain.Users;
using Shared.Primitives;

namespace SearchBugs.Domain.Git;

public class PullRequest : Entity<PullRequestId>
{
    public string RepoUrl { get; private set; } = string.Empty;
    public string SourceBranch { get; private set; } = string.Empty;
    public string TargetBranch { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public PullRequestStatus Status { get; private set; }
    public UserId CreatedBy { get; private set; } = null!;
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? MergedAtUtc { get; private set; }
    public UserId? MergedBy { get; private set; }

    private PullRequest(PullRequestId id) : base(id) { }

    private PullRequest() : base() { }

    public static PullRequest Create(
        PullRequestId id,
        string repoUrl,
        string sourceBranch,
        string targetBranch,
        string title,
        string description,
        UserId createdBy)
    {
        var pr = new PullRequest(id)
        {
            RepoUrl = repoUrl,
            SourceBranch = sourceBranch,
            TargetBranch = targetBranch,
            Title = title,
            Description = description,
            Status = PullRequestStatus.Open,
            CreatedBy = createdBy,
            CreatedAtUtc = DateTime.UtcNow
        };
        return pr;
    }

    public void MarkMerged(UserId mergedBy)
    {
        Status = PullRequestStatus.Merged;
        MergedAtUtc = DateTime.UtcNow;
        MergedBy = mergedBy;
    }

    public void MarkClosed()
    {
        Status = PullRequestStatus.Closed;
    }
}
