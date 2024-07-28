using SearchBugs.Domain.Users;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Authentications.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Result<Email> email = Email.Create(request.Email);

        //Result<User> user = await _userRepository.GetUserByEmailAsync(email.Value, cancellationToken);
        //if (user.IsFailure)
        //    return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail(email.Value.Value));

        var user = User.Create(Name.Create("John", "Doe"), email.Value, "123");

        // TODO: Implement the login logic.
        string jwtToken = _jwtProvider.GenerateJwtToken(user);

        // Return the login response.
        return Result.Success(new LoginResponse(jwtToken));

    }
}
