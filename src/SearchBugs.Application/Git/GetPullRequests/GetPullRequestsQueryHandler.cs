using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetPullRequests;

internal sealed class GetPullRequestsQueryHandler : IQueryHandler<GetPullRequestsQuery, IEnumerable<PullRequestListItemDto>>
{
    private readonly IPullRequestRepository _pullRequestRepository;

    public GetPullRequestsQueryHandler(IPullRequestRepository pullRequestRepository)
    {
        _pullRequestRepository = pullRequestRepository;
    }

    public async Task<Result<IEnumerable<PullRequestListItemDto>>> Handle(GetPullRequestsQuery request, CancellationToken cancellationToken)
    {
        PullRequestStatus? status = request.Status?.ToLowerInvariant() switch
        {
            "open" => PullRequestStatus.Open,
            "merged" => PullRequestStatus.Merged,
            "closed" => PullRequestStatus.Closed,
            _ => null
        };
        var list = await _pullRequestRepository.ListByRepoUrlAsync(request.RepoUrl, status, cancellationToken);
        var dtos = list.Select(pr => new PullRequestListItemDto(
            pr.Id.Value,
            pr.Title,
            pr.SourceBranch,
            pr.TargetBranch,
            pr.Status.ToString(),
            pr.CreatedBy.Value,
            pr.CreatedAtUtc));
        return Result.Success(dtos.AsEnumerable());
    }
}
