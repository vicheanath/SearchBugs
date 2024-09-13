using SearchBugs.Domain.Repositories;

namespace SearchBugs.Infrastructure.Services;

public interface IGitRepositoryService
{
    Dictionary<string, GitTreeItem> GetFolderTree(string repositoryName, string folder);

    GitTreeItem GetRepositoryTree(string repoName);
}