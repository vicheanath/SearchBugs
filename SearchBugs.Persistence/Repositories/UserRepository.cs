using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain.Users;
using Shared.Results;

namespace SearchBugs.Persistence.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken) =>
          Result.Create(await DbContext.Users.FirstOrDefaultAsync(user => user.Email.Value == email, cancellationToken));
}
