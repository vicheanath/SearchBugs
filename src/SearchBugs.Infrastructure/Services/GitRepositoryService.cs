using LibGit2Sharp;
using Microsoft.Extensions.Options;
using SearchBugs.Domain.Repositories;
using SearchBugs.Infrastructure.Options;

namespace SearchBugs.Infrastructure.Services;

internal sealed class GitRepositoryService : IGitRepositoryService
{
    private readonly GitOptions _gitOptions;

    public GitRepositoryService(IOptions<GitOptions> gitOptions)
    {
        _gitOptions = gitOptions.Value;
    }

    public Dictionary<string, GitTreeItem> GetFolderTree(string repositoryName, string folder)
    {

        var gitPath = Path.Combine(_gitOptions.BasePath, repositoryName);
        using var repo = new LibGit2Sharp.Repository(gitPath);

        var tree = repo.Head.Tip.Tree;
        var treeItems = new Dictionary<string, GitTreeItem>();
        foreach (var entry in tree)
        {
            if (entry.TargetType == TreeEntryTargetType.Tree)
            {
                var date = DateTime.UtcNow;
                var shortMessageHtmlLink = "Test";
                var id = entry.Target.Id.Sha;
                var url = $"{repositoryName}/{folder}/{entry.Name}";
                treeItems.Add(entry.Name, GitTreeItem.Create(id, url, DateTime.Parse(date.ToString()), shortMessageHtmlLink));
            }
        }

        return treeItems;


    }

    public class TreeItem
    {

        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDirectory { get; set; }
        public List<TreeItem> Children { get; set; } = new List<TreeItem>();
        public TreeItem(string name, string path, bool isDirectory)
        {
            Name = name;
            Path = path;
            IsDirectory = isDirectory;
        }
        public void AddChild(TreeItem child) => Children.Add(child);
    }

    public TreeItem GetRepositoryTree(string repoName)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        using (var repo = new LibGit2Sharp.Repository(repoPath))
        {
            var rootTree = repo.Head.Tip.Tree;
            var rootItem = new TreeItem(
              repo.Info.WorkingDirectory,
               repo.Info.WorkingDirectory,
               true
            );

            PopulateTree(rootTree, rootItem, repo.Info.WorkingDirectory);
            return rootItem;
        }
    }

    private void PopulateTree(Tree tree, TreeItem parentItem, string parentPath)
    {
        foreach (var entry in tree)
        {
            var itemPath = Path.Combine(parentPath, entry.Path);
            if (entry.TargetType == TreeEntryTargetType.Tree)
            {
                var treeItem = new TreeItem(entry.Name, itemPath, true);
                parentItem.AddChild(treeItem);
                PopulateTree((Tree)entry.Target, treeItem, itemPath);
            }
            else
            {
                parentItem.AddChild(new TreeItem(entry.Name, itemPath, false));
            }
        }
    }

    GitTreeItem IGitRepositoryService.GetRepositoryTree(string repoName)
    {
        throw new NotImplementedException();
    }
}
