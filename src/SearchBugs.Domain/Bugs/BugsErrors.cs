using Shared.Errors;

namespace SearchBugs.Domain.Bugs;

internal static class BugsErrors
{
    internal static Error TitleIsRequired => new Error("Bug.TitleIsRequired", "Title is required");
    internal static Error InvalidBugStatus => new Error("Bug.InvalidStatus", "Invalid bug status");

    internal static Error InvalidBugPriority => new Error("Bug.InvalidPriority", "Invalid bug priority");

    internal static Error InvalidBugSeverity => new Error("Bug.InvalidSeverity", "Invalid bug severity");

    internal static Error InvalidProjectId => new Error("Bug.InvalidProjectId", "Invalid project id");

    internal static Error InvalidAssigneeId => new Error("Bug.InvalidAssigneeId", "Invalid assignee id");

    internal static Error InvalidReporterId => new Error("Bug.InvalidReporterId", "Invalid reporter id");

    internal static Error InvalidTitle => new Error("Bug.InvalidTitle", "Invalid title");
}
