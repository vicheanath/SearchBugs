using Shared.Data;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.BugTracking.GetBugs;

public sealed class GetBugsQueryHandler : IQueryHandler<GetBugsQuery, List<BugsResponse>>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public GetBugsQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;


    public Task<Result<List<BugsResponse>>> Handle(GetBugsQuery request, CancellationToken cancellationToken) =>
        Result.Create(request)
            .Bind(async query => Result.Create(await GetBugsByProjectIdAsync(query.ProjectId)))
            .Map(bugs => bugs.ToList());


    public async Task<IEnumerable<BugsResponse>?> GetBugsByProjectIdAsync(Guid projectId) =>
        await _sqlQueryExecutor.QueryAsync<BugsResponse>(@"
            SELECT 
                b.id as Id,
                b.title as Title,
                b.description as Description,
                s.name as Status,
                p.name as Priority,
                b.severity as Severity,
                p.name as ProjectName,
                CONCAT(u.name_first_name,' ',u.name_last_name) as Assignee,
                CONCAT(u.name_first_name,' ',u.name_last_name) as Reporter,
                b.created_on_utc as CreatedOnUtc,
                b.modified_on_utc as UpdatedOnUtc
            FROM bug b
            JOIN bug_status s ON b.status_id = s.id
            JOIN bug_priority p ON b.priority_id = p.id
            JOIN ""user"" u ON b.assignee_id = u.id
            ORDER BY b.created_on_utc DESC", new { projectId });
}
