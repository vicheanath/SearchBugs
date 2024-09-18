using Shared.Messaging;

namespace SearchBugs.Application.Git.GetCommitDiff;

public record CommitDiffResult(string FilePath, string OldPath, string Status, string Patch) : IQuery<IEnumerable<CommitDiffResult>>;
