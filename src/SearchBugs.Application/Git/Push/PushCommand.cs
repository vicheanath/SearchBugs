using Shared.Messaging;

namespace SearchBugs.Application.Git.Push;

public record PushCommand(string RepoUrl, string BranchName, string? RemoteName = "origin") : ICommand;
