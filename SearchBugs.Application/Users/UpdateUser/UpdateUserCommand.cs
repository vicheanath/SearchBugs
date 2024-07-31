using Shared.Messaging;

namespace SearchBugs.Application.Users.UpdateUser;

public record UpdateUserCommand(Guid Id, string Name) : ICommand;