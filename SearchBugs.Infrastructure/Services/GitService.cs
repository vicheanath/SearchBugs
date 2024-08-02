using LibGit2Sharp;
using Microsoft.Extensions.Options;
using SearchBugs.Domain.Repositories;
using SearchBugs.Infrastructure.Options;
namespace SearchBugs.Infrastructure.Services;

internal class GitService : IGitService
{
    private readonly GitOptions _gitOptions;

    public GitService(IOptions<GitOptions> gitOptions)
    {
        _gitOptions = gitOptions.Value;
    }

    public MemoryStream GetInfoRefs(string repoName)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        using var repo = new LibGit2Sharp.Repository(repoPath);

        var references = repo.Refs;
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        foreach (var reference in references)
        {
            writer.Write($"{reference.TargetIdentifier} {reference.CanonicalName}\n");
        }
        writer.Flush();
        memoryStream.Position = 0;
        return memoryStream;
    }

    public void ReceivePack(string repoName, Stream stream)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        using var repo = new LibGit2Sharp.Repository(repoPath);
        string branchName = repo.Head.FriendlyName;
        Signature auth = new Signature("username", "email", DateTimeOffset.Now);
        Signature committer = new Signature("username", "email", DateTimeOffset.Now);

        string newCommitSha = "new_commit_sha"; // Replace with actual commit SHA from the pack file

        var branch = repo.Branches[branchName];
        if (branch != null)
        {
            var newCommit = repo.Lookup<Commit>(newCommitSha);
            repo.Refs.UpdateTarget(repo.Refs[branch.CanonicalName], newCommit.Sha);
        }
    }

    public MemoryStream UploadPack(string repoName, Stream stream)
    {
        var repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        using var repo = new LibGit2Sharp.Repository(repoPath);
        var commits = repo.Commits.ToList();
        var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(stream);

        foreach (var commit in commits)
        {
            writer.WriteLine(commit.Sha); // Write commit SHA
        }
        writer.Flush();

        return new MemoryStream();
    }


    public void CloneRepository(string repoUrl, string repoName)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        LibGit2Sharp.Repository.Clone(repoUrl, repoPath);
    }

    public void Fetch(string repoName, string username, string password)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        using (var repo = new LibGit2Sharp.Repository(repoPath))
        {
            var remote = repo.Network.Remotes["origin"];
            var options = new FetchOptions
            {
                CredentialsProvider = (url, usernameFromUrl, types) =>
                    new UsernamePasswordCredentials { Username = username, Password = password }
            };
            Commands.Fetch(repo, remote.Name, remote.FetchRefSpecs.Select(x => x.Specification), options, null);
        }
    }

    public void Push(string repoName, string username, string password)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        using (var repo = new LibGit2Sharp.Repository(repoPath))
        {
            var remote = repo.Network.Remotes["origin"];
            var options = new PushOptions
            {
                CredentialsProvider = (url, usernameFromUrl, types) =>
                    new UsernamePasswordCredentials { Username = username, Password = password }
            };
            repo.Network.Push(remote, @"refs/heads/master", options);
        }
    }

    public GitTreeItem GetRepositoryTree(string repoName)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        using (var repo = new LibGit2Sharp.Repository(repoPath))
        {
            var rootTree = repo.Head.Tip.Tree;
            var rootItem = GitTreeItem.Create(
                repo.Info.WorkingDirectory,
                repo.Info.WorkingDirectory,
                true
            );

            PopulateTree(rootTree, rootItem, repo.Info.WorkingDirectory);
            return rootItem;
        }
    }

    private void PopulateTree(Tree tree, GitTreeItem parentItem, string parentPath)
    {
        foreach (var entry in tree)
        {
            var itemPath = Path.Combine(parentPath, entry.Path);
            if (entry.TargetType == TreeEntryTargetType.Tree)
            {
                var treeItem = GitTreeItem.Create(entry.Name, itemPath, true);
                parentItem.AddChild(treeItem);
                PopulateTree((Tree)entry.Target, treeItem, itemPath);
            }
            else
            {
                parentItem.AddChild(GitTreeItem.Create(entry.Name, itemPath, false));
            }
        }
    }

    public void DeleteRepository(string repoName)
    {
        string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
        if (Directory.Exists(repoPath))
        {
            Directory.Delete(repoPath, true);
        }
        else
        {
            throw new DirectoryNotFoundException($"Repository '{repoName}' not found.");
        }
    }

    public void CreateRepository(string repoName)
    {
        try
        {
            string repoPath = Path.Combine(_gitOptions.BasePath, repoName);
            if (Directory.Exists(repoPath))
            {
                throw new InvalidOperationException($"Repository '{repoName}' already exists.");
            }

            LibGit2Sharp.Repository.Init(repoPath);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create repository '{repoName}'.", ex);
        }
    }
}



