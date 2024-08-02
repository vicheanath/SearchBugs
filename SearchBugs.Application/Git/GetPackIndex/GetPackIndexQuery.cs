using Shared.Messaging;

namespace SearchBugs.Application.Git.GetPackIndexQuery;

public record GetPackIndexQuery(string RepoName, string PackName) : IQuery<byte[]>;
