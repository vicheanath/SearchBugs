using Shared.Data;
using Shared.Errors;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Projects.GetProject;

internal sealed class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, GetProjectResponse>
{
    private static readonly Error ProjectNotFound = new("Project.NotFound", "Project not found");

    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public GetProjectQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    public async Task<Result<GetProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await GetProjectAsync(request.ProjectId);
        if (project is null)
            return Result.Failure<GetProjectResponse>(ProjectNotFound);
        return Result.Success(project);
    }

    private async Task<GetProjectResponse?> GetProjectAsync(Guid projectId) =>
        await _sqlQueryExecutor.FirstOrDefaultAsync<GetProjectResponse>(@"
            SELECT 
                p.id as Id,
                p.name as Name,
                p.description as Description,
                p.created_on_utc as CreatedOnUtc,
                p.modified_on_utc as UpdatedOnUtc
            FROM project p
            WHERE p.id = @ProjectId", new { ProjectId = projectId });
}
