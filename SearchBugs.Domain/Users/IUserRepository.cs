using Shared.Results;

namespace SearchBugs.Domain.Users;

public interface IUserRepository
{
    Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

    Task Add(User user);
}
