
using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetListTree;

internal sealed class GetListTreeQueryHandler : IQueryHandler<GetListTreeQuery, IEnumerable<GitTreeItemResult>>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public GetListTreeQueryHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }
    public Task<Result<IEnumerable<GitTreeItemResult>>> Handle(GetListTreeQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_gitRepositoryService.ListTree(request.CommitSha, request.Url)
            .Map(tree =>
            tree.Select(item => new GitTreeItemResult(item.Path, item.Name, item.Type, ""))));
    }
}
