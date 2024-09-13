using Shared.Errors;

namespace SearchBugs.Application.BugTracking;

internal static class BugValidationErrors
{
    internal static Error UserNotFound => new("Bug.StatusIsRequired", "The bug status is required.");
    internal static Error TitleIsRequired => new("Bug.TitleIsRequired", "The bug title is required.");
    internal static Error TitleMaxLength => new("Bug.TitleMaxLength", "The bug title must not exceed 100 characters.");
    internal static Error DescriptionIsRequired => new("Bug.DescriptionIsRequired", "The bug description is required.");
    internal static Error DescriptionMaxLength => new("Bug.DescriptionMaxLength", "The bug description must not exceed 500 characters.");
    internal static Error SeverityIsRequired => new("Bug.SeverityIsRequired", "The bug severity is required.");
    internal static Error ProjectIdIsRequired => new("Bug.ProjectIdIsRequired", "The project ID is required.");
    internal static Error AssigneeIdIsRequired => new("Bug.AssigneeIdIsRequired", "The assignee ID is required.");
    internal static Error ReporterIdIsRequired => new("Bug.ReporterIdIsRequired", "The reporter ID is required.");
    internal static Error PriorityIsRequired => new("Bug.PriorityIsRequired", "The bug priority is required.");
    internal static Error InvalidBugStatus => new("Bug.InvalidBugStatus", "The bug status is invalid.");
    internal static Error InvalidBugPriority => new("Bug.InvalidBugPriority", "The bug priority is invalid.");


}
