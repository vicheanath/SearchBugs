using Shared.Data;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetGitRepo;

internal sealed class GetGitRepoQueryHandler : IQueryHandler<GetGitRepoQuery, List<GetGitRepoQueryResult>>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;
    public GetGitRepoQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    public Task<Result<List<GetGitRepoQueryResult>>> Handle(GetGitRepoQuery request, CancellationToken cancellationToken) =>
       Result.Create(request)
        .Bind(async query => Result.Create(await GetGitRepoAsync()))
        .Map(repo => repo.ToList());

    private async Task<IEnumerable<GetGitRepoQueryResult>?> GetGitRepoAsync() =>
        await _sqlQueryExecutor.QueryAsync<GetGitRepoQueryResult>(@"
            SELECT 
                r.id as Id,
                r.name as Name,
                r.description as Description,
                r.url as Url,
                r.created_at as CreatedOnUtc
            FROM repository r");

}
