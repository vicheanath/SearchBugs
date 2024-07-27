
using SearchBugs.Domain;
using SearchBugs.Domain.BugTracking;
using SearchBugs.Domain.Project;
using SearchBugs.Domain.User;
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

        var bug = Bug.Create(
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            request.Severity,
            new ProjectId(request.ProjectId),
            new UserId(request.AssigneeId),
            new UserId(request.ReporterId));

        _bugRepository.Add(bug);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
