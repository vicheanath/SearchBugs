using Shared.Messaging;

namespace SearchBugs.Application.Git.DeleteGitRepo;

public sealed class DeleteGitRepoCommand(string Url) : ICommand;
