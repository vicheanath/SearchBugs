
using LibGit2Sharp;
using Microsoft.Extensions.Options;
using SearchBugs.Infrastructure.Options;

namespace SearchBugs.Infrastructure.Services;

internal class LibGitService : ILibGitService
{
    private readonly GitOptions _gitOptions;

    private string _repoPath => _gitOptions.BasePath;

    public LibGitService(IOptions<GitOptions> gitOptions)
    {
        _gitOptions = gitOptions.Value;
    }

    public Repository GetRepoByName(string repoName)
    {
        var repoPath = Path.Combine(_repoPath, repoName);
        using var repo = new Repository(repoPath);
        return repo;
    }
}
