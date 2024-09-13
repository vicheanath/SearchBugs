using MediatR;
using SearchBugs.Domain.Git;
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

    Task<Result<Dictionary<string, GitRepoItem>>> IRequestHandler<GetGitReposDetailsQuery, Result<Dictionary<string, GitRepoItem>>>.Handle(GetGitReposDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
