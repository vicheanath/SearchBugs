
using Shared.Data;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Projects.GetProjects;

internal sealed class GetProjectsQueryHandler : IQueryHandler<GetProjectsQuery, List<GetProjectsResponse>>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public GetProjectsQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    public Task<Result<List<GetProjectsResponse>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken) =>
        Result.Create(request)
            .Bind(async query => Result.Create(await GetProjectsAsync()))
            .Map(projects => projects.ToList());

    private async Task<IEnumerable<GetProjectsResponse>?> GetProjectsAsync() =>
        await _sqlQueryExecutor.QueryAsync<GetProjectsResponse>(@"
            SELECT 
                p.id as Id,
                p.name as Name,
                p.description as Description,
                p.created_on_utc as CreatedOnUtc,
                p.modified_on_utc as ModifiedOnUtc
            FROM project p");

}
