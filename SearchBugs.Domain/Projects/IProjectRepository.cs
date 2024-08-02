using Shared.Results;

namespace SearchBugs.Domain.Projects;

public interface IProjectRepository
{
    Task Add(Project project);

    Task<Result<Project>> GetByIdAsync(ProjectId projectId, CancellationToken cancellationToken);
}
