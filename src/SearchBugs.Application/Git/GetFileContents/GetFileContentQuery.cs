using Shared.Messaging;

namespace SearchBugs.Application.Git.GetFileContents;

public record GetFileContentQuery(string Url, string CommitSha, string FilePath) : IQuery<string>;
