namespace SearchBugs.Domain.Git;

public record MergeResult
{
    public string Status { get; init; } = string.Empty;
    public string CommitSha { get; init; } = string.Empty;
}



