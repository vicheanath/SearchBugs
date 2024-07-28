using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Users;

namespace SearchBugs.Application.BugTracking.GetBugs;

public sealed record BugsResponse(
        string Title,
        string Description,
        string Status,
        string Priority,
        string Severity,
        ProjectId ProjectId,
        UserId AssigneeId,
        UserId ReporterId,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
