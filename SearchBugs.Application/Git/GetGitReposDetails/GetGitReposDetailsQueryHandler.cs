using SearchBugs.Infrastructure.Services;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetGitReposDetails;

internal sealed class GetGitReposDetailsQueryHandler : IQueryHandler<GetGitReposDetailsQuery, Dictionary<string, GitRepoItem>>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public GetGitReposDetailsQueryHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result<Dictionary<string, GitRepoItem>>> Handle(GetGitReposDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = _gitRepositoryService.GetFolderTree(request.RepoName, request.FolderName);
        var mappedResult = result.ToDictionary(
            x => x.Key,
            x => new GitRepoItem(x.Value.Id, x.Value.Url, x.Value.Date, x.Value.ShortMessageHtmlLink)
            );
        return Task.FromResult(Result.Success(mappedResult));
    }
}
