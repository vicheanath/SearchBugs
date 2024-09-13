using Shared.Messaging;

namespace SearchBugs.Application.Projects.GetProjects;

public record GetProjectsQuery() : IQuery<List<GetProjectsResponse>>;
