using Shared.Data;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<GetUsersResponse>>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;
    public GetUsersQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;
    public Task<Result<List<GetUsersResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken) =>
        Result.Create(request)
            .Bind(async query => Result.Create(await GetUsersAsync()))
            .Map(users => users.ToList());
    public async Task<IEnumerable<GetUsersResponse>?> GetUsersAsync() =>
        await _sqlQueryExecutor.QueryAsync<GetUsersResponse>(@"
            SELECT 
                u.id as Id,
                u.name_first_name as FirstName,
                u.name_last_name as LastName,
                u.email_value as Email,
                u.created_on_utc as CreatedOnUtc,
                u.modified_on_utc as ModifiedOnUtc
            FROM ""user"" u");
}
