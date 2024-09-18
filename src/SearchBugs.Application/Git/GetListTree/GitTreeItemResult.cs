namespace SearchBugs.Application.Git.GetListTree;

public record GitTreeItemResult(string Path, string Name, string Type, string Url);