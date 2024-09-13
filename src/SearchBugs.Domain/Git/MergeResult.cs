namespace SearchBugs.Infrastructure.Services;


public record MergeResult
{
    public string Status { get; init; }
    public string CommitSha { get; init; }
}



