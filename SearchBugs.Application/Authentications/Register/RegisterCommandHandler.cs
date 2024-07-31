using SearchBugs.Domain;
using SearchBugs.Domain.Roles;
using SearchBugs.Domain.Services;
using SearchBugs.Domain.Users;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Authentications.Register;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService passwordHashingService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IPasswordHashingService passwordHashingService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        this.passwordHashingService = passwordHashingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
                Name.Create(request.FirstName, request.LastName),
                Email.Create(request.Email),
                passwordHashingService.HashPassword(request.Password));

        var role = await _userRepository.GetRoleByIdAsync(Role.Guest.Id, cancellationToken);
        user.Value.AddRole(role.Value);
        await _userRepository.Add(user.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }

    private async Task<Result<User>> CheckIfEmailIsUniqueAsync(User user, CancellationToken cancellationToken) =>
                await _userRepository.IsEmailUniqueAsync(user.Email, cancellationToken);
}
