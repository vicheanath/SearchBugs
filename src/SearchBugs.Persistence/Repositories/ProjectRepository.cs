using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain.Projects;
using Shared.Results;

namespace SearchBugs.Persistence.Repositories;

internal class ProjectRepository : Repository<Project, ProjectId>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<Project>> GetByIdAsync(ProjectId projectId, CancellationToken cancellationToken) =>
        Result.Create(await _context.Set<Project>().FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken));
}
