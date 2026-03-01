using MediatR;
using SearchBugs.Domain.Git;
using Shared.Errors;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetGitReposDetails;

internal sealed class GetGitReposDetailsQueryHandler : IQueryHandler<GetGitReposDetailsQuery, Dictionary<string, GitRepoItem>>
{
    private static readonly Error NotImplemented = new("GitReposDetails.NotImplemented", "This feature is not yet implemented.");

    private readonly IGitRepositoryService _gitRepositoryService;

    public GetGitReposDetailsQueryHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result<Dictionary<string, GitRepoItem>>> Handle(GetGitReposDetailsQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(Result.Failure<Dictionary<string, GitRepoItem>>(NotImplemented));
}
