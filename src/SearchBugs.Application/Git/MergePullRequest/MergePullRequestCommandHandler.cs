using SearchBugs.Domain;
using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.MergePullRequest;

internal sealed class MergePullRequestCommandHandler : ICommandHandler<MergePullRequestCommand>
{
    private readonly IPullRequestRepository _pullRequestRepository;
    private readonly IGitRepositoryService _gitRepositoryService;
    private readonly IUnitOfWork _unitOfWork;

    public MergePullRequestCommandHandler(
        IPullRequestRepository pullRequestRepository,
        IGitRepositoryService gitRepositoryService,
        IUnitOfWork unitOfWork)
    {
        _pullRequestRepository = pullRequestRepository;
        _gitRepositoryService = gitRepositoryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(MergePullRequestCommand request, CancellationToken cancellationToken)
    {
        var pr = await _pullRequestRepository.GetByIdAsync(new PullRequestId(request.PullRequestId), cancellationToken);
        if (pr == null || pr.RepoUrl != request.RepoUrl)
            return Result.Failure(PullRequestErrors.NotFound);
        if (pr.Status != PullRequestStatus.Open)
            return Result.Failure(PullRequestErrors.AlreadyMerged);

        var mergeResult = _gitRepositoryService.MergeBranches(
            request.RepoUrl,
            pr.SourceBranch,
            pr.TargetBranch,
            request.AuthorName,
            request.AuthorEmail);
        if (mergeResult.IsFailure)
            return mergeResult;

        pr.MarkMerged(new Domain.Users.UserId(request.MergedByUserId));
        _pullRequestRepository.Update(pr);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
