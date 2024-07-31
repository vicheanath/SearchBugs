using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Users;
using Shared.Primitives;
using Shared.Results;
using Shared.Time;

namespace SearchBugs.Domain.Bugs;

public class Bug : Entity<BugId>, IAuditable
{
    public string Title { get; }
    public string Description { get; }
    public int StatusId { get; }
    public int PriorityId { get; }
    public BugStatus Status { get; private set; }
    public BugPriority Priority { get; private set; }
    public string Severity { get; private set; }
    public ProjectId ProjectId { get; }
    public UserId AssigneeId { get; }

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


    private Bug(BugId id, string title, string description, BugStatus status, BugPriority priority, string severity, ProjectId projectId, UserId assigneeId, UserId reporterId, DateTime createdOnUtc) : base(id)
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        Severity = severity;
        ProjectId = projectId;
        AssigneeId = assigneeId;
        ReporterId = reporterId;
        CreatedOnUtc = createdOnUtc;
    }

    private Bug()
    {

    }

    public static Result<Bug> Create(string title, string description, BugStatus status, BugPriority priority, string severity, ProjectId projectId, UserId assigneeId, UserId reporterId)
    {
        BugId bugId = new(Guid.NewGuid());
        return Result.Create(new Bug(bugId, title, description, status, priority, severity, projectId, assigneeId, reporterId, SystemTime.UtcNow));
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
}
