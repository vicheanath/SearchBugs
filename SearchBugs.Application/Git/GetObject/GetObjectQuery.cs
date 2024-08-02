using Shared.Messaging;

namespace SearchBugs.Application.Git.GetObjectQuery;

public record GetObjectQuery(string RepoName, string ObjectName) : IQuery<byte[]>;

