namespace SearchBugs.Infrastructure.Services;

public record GitTreeItem
{
    public string Path { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
}
