using Shared.Messaging;

namespace SearchBugs.Application.Git.GitHttpServer;

public record GitHttpServerCommand(string Name, CancellationToken CancellationToken) : ICommand;
