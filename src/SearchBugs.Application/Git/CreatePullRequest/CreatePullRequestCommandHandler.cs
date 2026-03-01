using SearchBugs.Domain;
using SearchBugs.Domain.Git;
using SearchBugs.Domain.Users;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.CreatePullRequest;

internal sealed class CreatePullRequestCommandHandler : ICommandHandler<CreatePullRequestCommand, PullRequestDto>
{
    private readonly IPullRequestRepository _pullRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGitRepositoryService _gitRepositoryService;

    public CreatePullRequestCommandHandler(
        IPullRequestRepository pullRequestRepository,
        IUnitOfWork unitOfWork,
        IGitRepositoryService gitRepositoryService)
    {
        _pullRequestRepository = pullRequestRepository;
        _unitOfWork = unitOfWork;
        _gitRepositoryService = gitRepositoryService;
    }

    public async Task<Result<PullRequestDto>> Handle(CreatePullRequestCommand request, CancellationToken cancellationToken)
    {
        var branchesResult = _gitRepositoryService.GetBranches(request.RepoUrl);
        if (branchesResult.IsFailure)
            return Result.Failure<PullRequestDto>(branchesResult.Error);
        var branches = branchesResult.Value.ToList();
        if (!branches.Contains(request.SourceBranch))
            return Result.Failure<PullRequestDto>(PullRequestErrors.BranchNotFound);
        if (!branches.Contains(request.TargetBranch))
            return Result.Failure<PullRequestDto>(PullRequestErrors.BranchNotFound);

        var id = new PullRequestId(Guid.NewGuid());
        var createdBy = new UserId(request.CreatedByUserId);
        var pr = PullRequest.Create(
            id,
            request.RepoUrl,
            request.SourceBranch,
            request.TargetBranch,
            request.Title,
            request.Description,
            createdBy);
        _pullRequestRepository.Add(pr);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ToDto(pr);
    }

    private static PullRequestDto ToDto(PullRequest pr) => new(
        pr.Id.Value,
        pr.RepoUrl,
        pr.SourceBranch,
        pr.TargetBranch,
        pr.Title,
        pr.Description,
        pr.Status.ToString(),
        pr.CreatedBy.Value,
        pr.CreatedAtUtc,
        pr.MergedAtUtc,
        pr.MergedBy?.Value);
}
