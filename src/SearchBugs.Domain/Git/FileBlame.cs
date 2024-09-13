namespace SearchBugs.Infrastructure.Services;

public record FileBlame
{
    public int LineNumber { get; init; }
    public string CommitSha { get; init; }
    public string Author { get; init; }
    public string Email { get; init; }
    public DateTime Date { get; init; }
    public int LineContent { get; init; }
}

