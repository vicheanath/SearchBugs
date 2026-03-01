using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetCommits;

internal sealed class GetCommitsQueryHandler : IQueryHandler<GetCommitsQuery, IEnumerable<CommitInfoDto>>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public GetCommitsQueryHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result<IEnumerable<CommitInfoDto>>> Handle(GetCommitsQuery request, CancellationToken cancellationToken)
    {
        var result = _gitRepositoryService.GetCommits(
            request.RepoUrl,
            request.BranchName,
            request.Skip,
            request.Take);
        return Task.FromResult(result.Map(commits =>
            commits.Select(c => new CommitInfoDto(c.Sha, c.Message, c.AuthorName, c.AuthorEmail, c.When)).AsEnumerable()));
    }
}
