using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain.Roles;
using SearchBugs.Domain.Users;
using Shared.Results;

namespace SearchBugs.Persistence.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Result<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Role>> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken) =>
          Result.Create(await DbContext.Set<Role>().FirstOrDefaultAsync(role => role.Id == roleId, cancellationToken));

    public async Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken) =>
          Result.Create(await DbContext.Set<User>().FirstOrDefaultAsync(user => user.Email.Value == email, cancellationToken));

    public async Task<Result<User>> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken) =>
        Result.Create(await DbContext.Set<User>().FirstOrDefaultAsync(user => user.Email.Value == email.Value, cancellationToken));
}
