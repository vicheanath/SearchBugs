using SearchBugs.Domain.Bugs.Events;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Users;
using Shared.Primitives;
using Shared.Results;
using Shared.Time;

namespace SearchBugs.Domain.Bugs;

public class Bug : Entity<BugId>, IAuditable
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int StatusId { get; private set; }
    public int PriorityId { get; private set; }
    public BugStatus Status { get; private set; }
    public BugPriority Priority { get; private set; }
    public string Severity { get; private set; }
    public ProjectId ProjectId { get; }
    public UserId AssigneeId { get; private set; }
    public UserId ReporterId { get; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }

    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();
    public IReadOnlyCollection<BugHistory> BugHistories => _bugHistories.AsReadOnly();
    public IReadOnlyCollection<BugCustomField> BugCustomFields => _bugCustomFields.AsReadOnly();
    public IReadOnlyCollection<TimeTracking> TimeTracking => _timeTracking.AsReadOnly();

    private readonly List<Comment> _comments = new();

    private readonly List<Attachment> _attachments = new();

    private readonly List<BugHistory> _bugHistories = new();

    private readonly List<BugCustomField> _bugCustomFields = new();

    private readonly List<TimeTracking> _timeTracking = new();


    private Bug(BugId id, string title, string description, int statusId, int priorityId, string severity, ProjectId projectId, UserId assigneeId, UserId reporterId, DateTime createdOnUtc) : base(id)
    {
        Title = title;
        Description = description;
        StatusId = statusId;
        PriorityId = priorityId;
        Severity = severity;
        ProjectId = projectId;
        AssigneeId = assigneeId;
        ReporterId = reporterId;
        CreatedOnUtc = createdOnUtc;
    }

    private Bug()
    {
        Title = string.Empty;
        Description = string.Empty;
        Severity = string.Empty;
        Status = null!;
        Priority = null!;
        ProjectId = null!;
        AssigneeId = null!;
        ReporterId = null!;
    }

    public static Result<Bug> Create(string title, string description, int status, int priority, string severity, ProjectId projectId, UserId assigneeId, UserId reporterId)
    {
        BugId bugId = new(Guid.NewGuid());
        var bug = new Bug(bugId, title, description, status, priority, severity, projectId, assigneeId, reporterId, SystemTime.UtcNow);

        // Raise domain event for bug creation
        bug.RaiseDomainEvent(new BugCreatedDomainEvent(
            bugId,
            title,
            description,
            assigneeId,
            reporterId,
            SystemTime.UtcNow
        ));

        return Result.Create(bug);
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public void AddAttachment(Attachment attachment)
    {
        _attachments.Add(attachment);
    }

    public void AddBugHistory(BugHistory bugHistory)
    {
        _bugHistories.Add(bugHistory);
    }

    public void AddBugCustomField(BugCustomField bugCustomField)
    {
        _bugCustomFields.Add(bugCustomField);
    }

    public void AddTimeTracking(TimeTracking timeTracking)
    {
        _timeTracking.Add(timeTracking);
    }

    public void UpdateStatus(BugStatus status)
    {
        Status = status;
    }

    public void UpdatePriority(BugPriority priority)
    {
        Priority = priority;
    }

    public Result Update(
        string title,
        string description,
        BugStatus status,
        BugPriority priority,
        string severity,
        UserId? assigneeId,
        UserId updatedBy)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure(BugsErrors.InvalidTitle);

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure(BugsErrors.InvalidDescription);

        if (string.IsNullOrWhiteSpace(severity))
            return Result.Failure(BugsErrors.InvalidBugSeverity);

        var previousAssigneeId = AssigneeId;

        Title = title;
        Description = description;
        Status = status;
        StatusId = status.Id;
        Priority = priority;
        PriorityId = priority.Id;
        Severity = severity;

        if (assigneeId != null)
        {
            AssigneeId = assigneeId;

            // Raise assignment event if assignee changed
            if (previousAssigneeId != assigneeId)
            {
                RaiseDomainEvent(new BugAssignedDomainEvent(
                    Id,
                    Title,
                    assigneeId,
                    updatedBy,
                    SystemTime.UtcNow
                ));
            }
        }

        ModifiedOnUtc = SystemTime.UtcNow;

        // Raise domain event for bug update
        RaiseDomainEvent(new BugUpdatedDomainEvent(
            Id,
            Title,
            Description,
            AssigneeId,
            updatedBy,
            SystemTime.UtcNow,
            "Updated"
        ));

        return Result.Success();
    }
}
