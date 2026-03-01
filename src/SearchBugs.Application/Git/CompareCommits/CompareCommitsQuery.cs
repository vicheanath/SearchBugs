using SearchBugs.Application.Git.GetCommitDiff;
using Shared.Messaging;

namespace SearchBugs.Application.Git.CompareCommits;

public record CompareCommitsQuery(string RepoUrl, string BaseCommitSha, string CompareCommitSha)
    : IQuery<IEnumerable<CommitDiffResult>>;
