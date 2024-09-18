using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.CommitChanges;

internal sealed class CommitChangeCommandHandler : ICommandHandler<CommitChangeCommand>
{
    private readonly IGitRepositoryService _gitRepositoryService;
    public CommitChangeCommandHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }
    public Task<Result> Handle(CommitChangeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_gitRepositoryService.CommitChanges(request.Url, request.AuthorName, request.AuthorEmail, request.CommitMessage));
    }
}
