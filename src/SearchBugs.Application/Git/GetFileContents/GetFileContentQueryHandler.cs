using SearchBugs.Domain.Git;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.GetFileContents;

internal sealed class GetFileContentQueryHandler : IQueryHandler<GetFileContentQuery, string>
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public GetFileContentQueryHandler(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
    }

    public Task<Result<string>> Handle(GetFileContentQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_gitRepositoryService.GetFileContent(request.Url, request.CommitSha, request.FilePath));
    }
}
