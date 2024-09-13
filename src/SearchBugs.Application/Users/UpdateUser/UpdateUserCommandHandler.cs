using SearchBugs.Domain;
using SearchBugs.Domain.Users;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(new UserId(request.Id), cancellationToken);
        if (user.IsFailure)
        {
            return Result.Failure(user.Error);
        }
        user.Value.Update(Name.Create(request.FirstName, request.LastName));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
