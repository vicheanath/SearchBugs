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


    public async Task<IEnumerable<BugsResponse>> GetBugsByProjectIdAsync(Guid projectId)
    {
        var query = @"
            SELECT 
                b.Title,
                b.Description,
                b.Status,
                b.Priority,
                b.Severity,
                b.ProjectId,
                b.AssigneeId,
                b.ReporterId,
                b.CreatedAt,
                b.UpdatedAt
            FROM Bugs b
            WHERE b.ProjectId = @ProjectId
            ORDER BY b.CreatedAt DESC";

        var bugs = await _sqlQueryExecutor.QueryAsync<BugsResponse>(query, new { projectId });

        return bugs;

    }
}
