using Shared.Messaging;

namespace SearchBugs.Application.Git.GetGitReposDetails;

public record GetGitReposDetailsQuery(
        string RepoName,
        string FolderName
    ) : IQuery<Dictionary<string, GitRepoItem>>;

