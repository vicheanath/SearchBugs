using Shared.Messaging;

namespace SearchBugs.Application.Git.DeleteGitRepo;

public record DeleteGitRepoCommand(string Url) : ICommand;
