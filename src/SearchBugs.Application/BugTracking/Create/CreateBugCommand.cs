

using Shared.Messaging;

namespace SearchBugs.Application.BugTracking.Create;

public record CreateBugCommand(
    string Title,
    string Description,
    string Status,
    string Priority,
    string Severity,
    Guid ProjectId,
    Guid AssigneeId,
    Guid ReporterId) : ICommand;
