

using Shared.Messaging;

namespace SearchBugs.Application.Git.GetCommitDiff;

public record GetCommitDiffQuery(string Url, string CommitSha) : IQuery<IEnumerable<CommitDiffResult>>;