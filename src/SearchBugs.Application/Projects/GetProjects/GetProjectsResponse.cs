namespace SearchBugs.Application.Projects.GetProjects;

public record GetProjectsResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc
    );