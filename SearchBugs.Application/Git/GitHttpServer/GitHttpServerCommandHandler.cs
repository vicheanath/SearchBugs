using SearchBugs.Domain.Repositories;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GitHttpServer;

internal sealed class GitHttpServerCommandHandler : ICommandHandler<GitHttpServerCommand>
{
    private readonly IGitService _gitService;

    public GitHttpServerCommandHandler(IGitService gitService) => _gitService = gitService;

    public async Task<Result> Handle(GitHttpServerCommand request, CancellationToken cancellationToken)
    {
        await _gitService.Handle(request.Name, cancellationToken);
        return Result.Success();
    }
}
