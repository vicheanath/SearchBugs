using SearchBugs.Domain;
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
        Email email = Email.Create(request.Email);
        Name name = Name.Create(request.FirstName, request.LastName);
        string password = passwordHashingService.HashPassword(request.Password);
        var user = User.Create(name, email, password);
        await _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
