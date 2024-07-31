using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{

    public Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
