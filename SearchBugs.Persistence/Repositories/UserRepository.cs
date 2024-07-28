using SearchBugs.Domain.Users;
using Shared.Results;

namespace SearchBugs.Persistence.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Result<User>> GetUserByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
