using SearchBugs.Application.Git.GetInfoRefsQuery;
using SearchBugs.Domain.Repositories;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetInfoRefs;

internal sealed class GetInfoRefsQueryHandler : IQueryHandler<InfoRefsQuery, MemoryStream>
{
    private readonly IGitService _gitService;

    public GetInfoRefsQueryHandler(IGitService gitService) => _gitService = gitService;
    public Task<Result<MemoryStream>> Handle(InfoRefsQuery request, CancellationToken cancellationToken)
    {
        var stream = _gitService.GetInfoRefs(request.RepoName);

        return Task.FromResult(Result.Success(stream));
    }
}
