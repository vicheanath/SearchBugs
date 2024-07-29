using SearchBugs.Domain.Services;
using SearchBugs.Domain.Users;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Authentications.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHashingService passwordHashingService;


    public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHashingService passwordHashingService)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        this.passwordHashingService = passwordHashingService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Email email = Email.Create(request.Email);

        Result<User> user = await _userRepository.GetUserByEmailAsync(email.Value, cancellationToken);
        if (user.IsFailure)
            return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail(email.Value));

        if (!isPasswordValid(request.Password, user.Value.Password))
            return Result.Failure<LoginResponse>(UserErrors.InvalidPassword);
        string jwtToken = _jwtProvider.GenerateJwtToken(user.Value);
        return Result.Success(new LoginResponse(jwtToken));
    }

    private bool isPasswordValid(string password, string hashedPassword)
    {
        return passwordHashingService.VerifyPassword(password, hashedPassword);
    }
}
