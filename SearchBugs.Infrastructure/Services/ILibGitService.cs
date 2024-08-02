using LibGit2Sharp;

namespace SearchBugs.Infrastructure.Services;

internal interface ILibGitService
{
    Repository GetRepoByName(string repoName);
}