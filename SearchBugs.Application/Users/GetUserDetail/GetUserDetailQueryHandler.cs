using SearchBugs.Domain.Users;
using Shared.Data;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Users.GetUserDetail;

public sealed class GetUserDetailQueryHandler : IQueryHandler<GetUserDetailQuery, GetUserDetailResponse>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public GetUserDetailQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    public async Task<Result<GetUserDetailResponse>> Handle(GetUserDetailQuery query, CancellationToken cancellationToken) =>
            await Result.Success(query)
            .Bind(async query => Result.Create(await GetUserDetailByIdAsync(query.UserId)))
            .MapFailure(() => UserErrors.NotFound(new UserId(query.UserId)));

    public async Task<GetUserDetailResponse?> GetUserDetailByIdAsync(Guid UserId) =>
        await _sqlQueryExecutor.FirstOrDefaultAsync<GetUserDetailResponse>(@"
            SELECT 
                u.id as Id,
                u.name_first_name as FirstName,
                u.name_last_name as LastName,
                u.email_value as Email,
                r.name as Roles,
                u.created_on_utc as CreatedOnUtc,
                u.modified_on_utc as ModifiedOnUtc
            FROM ""user"" u
            LEFT JOIN user_role ur ON u.id = ur.user_id
            LEFT JOIN ""role"" r ON ur.role_id = r.id
            WHERE u.id = @UserId", new { UserId });
}