

using Shared.Messaging;

namespace SearchBugs.Application.Projects.CreateProject;

public record CreateProjectCommand(
    string Name,
    string Description
    ) : ICommand;
