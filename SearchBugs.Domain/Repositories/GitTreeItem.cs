using Shared.Primitives;

namespace SearchBugs.Domain.Repositories;

public class GitTreeItem : ValueObject
{
    public string Name { get; set; }
    public string Path { get; set; }
    public bool IsDirectory { get; set; }
    public List<GitTreeItem> Children { get; set; } = new List<GitTreeItem>();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return Path;
        yield return IsDirectory;
        yield return Children;
    }

    private GitTreeItem(string name, string path, bool isDirectory)
    {
        Name = name;
        Path = path;
        IsDirectory = isDirectory;
    }

    public static GitTreeItem Create(string name, string path, bool isDirectory)
    {
        return new GitTreeItem(name, path, isDirectory);
    }

    public void AddChild(GitTreeItem child)
    {
        Children.Add(child);
    }
}
