using Shared.Results;

namespace SearchBugs.Domain.Users;

public interface IUserRepository
{
    Task<Result<User>> GetUserByEmailAsync(Email email, CancellationToken cancellationToken);
}
