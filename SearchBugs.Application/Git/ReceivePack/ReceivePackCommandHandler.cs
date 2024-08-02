using SearchBugs.Domain.Repositories;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.ReceivePack;

internal sealed class ReceivePackCommandHandler : ICommandHandler<ReceivePackCommand>
{
    private readonly IGitService _gitService;

    public ReceivePackCommandHandler(IGitService gitService) => _gitService = gitService;
    public Task<Result> Handle(ReceivePackCommand request, CancellationToken cancellationToken)
    {
        var stream = request.OutPut;
        _gitService.ReceivePack(request.RepoName, stream);

    }
}
