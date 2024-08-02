using Shared.Messaging;

namespace SearchBugs.Application.Git.ReceivePack;

public record ReceivePackCommand(string RepoName, Stream OutPut) : ICommand;

