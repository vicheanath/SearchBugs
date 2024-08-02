namespace SearchBugs.Domain.Repositories;

public interface IGitService
{
    MemoryStream GetInfoRefs(string repoName);
    void CloneRepository(string repoUrl, string repoName);
    void CreateRepository(string repoName);
    void DeleteRepository(string repoName);
    void Fetch(string repoName, string username, string password);
    GitTreeItem GetRepositoryTree(string repoName);
    void Push(string repoName, string username, string password);


}
