using SearchBugs.Domain.Roles;
using Shared.Results;

namespace SearchBugs.Domain.Users;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

    Task<Result<Role>> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken);

    Task<Result<User>> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken);
}
