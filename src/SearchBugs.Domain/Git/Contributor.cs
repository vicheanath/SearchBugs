namespace SearchBugs.Infrastructure.Services;

public record Contributor
{
    public string Name { get; init; }
    public string Email { get; init; }
    public int CommitCount { get; init; }
}

