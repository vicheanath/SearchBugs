using Shared.Messaging;

namespace SearchBugs.Application.Git.Pull;

public record PullCommand(
    string RepoUrl,
    string BranchName,
    string AuthorName,
    string AuthorEmail,
    string? RemoteName = "origin") : ICommand;
