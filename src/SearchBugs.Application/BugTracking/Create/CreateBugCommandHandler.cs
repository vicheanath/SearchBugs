
using SearchBugs.Domain;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Users;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.BugTracking.Create;

public class CreateBugCommandHandler : ICommandHandler<CreateBugCommand>
{
    private readonly IBugRepository _bugRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBugCommandHandler(IBugRepository bugRepository, IUnitOfWork unitOfWork)
    {
        _bugRepository = bugRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateBugCommand request, CancellationToken cancellationToken)
    {
        var bugStatus = BugStatus.FromName(request.Status);
        var bugPriority = BugPriority.FromName(request.Priority);
        if (bugStatus is null)
            return Result.Failure(BugValidationErrors.InvalidBugStatus);
        if (bugPriority is null)
            return Result.Failure(BugValidationErrors.InvalidBugPriority);

        var bug = Bug.Create(
            request.Title,
            request.Description,
            bugStatus.Id,
            bugPriority.Id,
            request.Severity,
            new ProjectId(request.ProjectId),
            new UserId(request.AssigneeId),
            new UserId(request.ReporterId));

        if (bug.IsFailure)
        {
            return Result.Failure(bug.Error);
        }

        await _bugRepository.Add(bug.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
