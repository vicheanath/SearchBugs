using Shared.Messaging;

namespace SearchBugs.Application.Git.CreateGitRepo;

public record CreateGitRepoCommand(
    string Name,
    string Description,
    string Url,
    Guid ProjectId
   ) : ICommand;