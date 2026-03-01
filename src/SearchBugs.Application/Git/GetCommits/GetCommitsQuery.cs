using Shared.Messaging;

namespace SearchBugs.Application.Git.GetCommits;

public record GetCommitsQuery(string RepoUrl, string? BranchName, int Skip = 0, int Take = 50)
    : IQuery<IEnumerable<CommitInfoDto>>;
