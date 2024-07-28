using Shared.Messaging;

namespace SearchBugs.Application.Authentications.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;
