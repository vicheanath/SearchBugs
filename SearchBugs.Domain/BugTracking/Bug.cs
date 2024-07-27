using SearchBugs.Domain.Project;
using SearchBugs.Domain.User;
using Shared.Primitives;

namespace SearchBugs.Domain.BugTracking;

public class Bug : Entity<BugId>
{
    public string Title { get; }
    public string Description { get; }
    public string Status { get; }
    public string Priority { get; }
    public string Severity { get; }
    public ProjectId ProjectId { get; }
    public UserId AssigneeId { get; }

    public UserId ReporterId { get; }

    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; } = DateTime.UtcNow;

    private readonly List<Comment> _comments = new();

    private readonly List<Attachment> _attachments = new();

    private readonly List<BugHistory> _bugHistories = new();

    private readonly List<BugCustomField> _bugCustomFields = new();

    private readonly List<TimeTracking> _timeTracking = new();


    private Bug(BugId id, string title, string description, string status, string priority, string severity, ProjectId projectId, UserId assigneeId, UserId reporterId)
        : base(id)
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        Severity = severity;
        ProjectId = projectId;
        AssigneeId = assigneeId;
        ReporterId = reporterId;
    }

    private Bug()
    {

    }

    public static Bug Create(string title, string description, string status, string priority, string severity, ProjectId projectId, UserId assigneeId, UserId reporterId)
    {
        var id = new BugId(Guid.NewGuid());
        return new Bug(id, title, description, status, priority, severity, projectId, assigneeId, reporterId);
    }
}
