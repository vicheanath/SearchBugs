namespace SearchBugs.Application.BugTracking.GetBugs;

public sealed record BugsResponse(
        Guid Id,
        string Title,
        string Description,
        string Status,
        string Priority,
        string Severity,
        string ProjectName,
        string Assignee,
        string Reporter,
        DateTime CreatedOnUtc,
        DateTime? UpdatedOnUtc
    );
