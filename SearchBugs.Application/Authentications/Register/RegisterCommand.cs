

using Shared.Messaging;

namespace SearchBugs.Application.Authentications.Register;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
    ) : ICommand;