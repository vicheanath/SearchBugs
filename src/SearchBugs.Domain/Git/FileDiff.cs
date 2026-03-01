namespace SearchBugs.Domain.Git;

public record FileDiff
{
    public string FilePath { get; init; } = string.Empty;
    public string OldPath { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Patch { get; init; } = string.Empty;
}

