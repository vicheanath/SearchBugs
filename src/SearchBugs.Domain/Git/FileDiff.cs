namespace SearchBugs.Infrastructure.Services;

public record FileDiff
{
    public string FilePath { get; init; }
    public string OldPath { get; init; }
    public string Status { get; init; }
    public string Patch { get; init; }
}

