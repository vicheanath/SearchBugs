using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Authentications.Register;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    public Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
