using Shared.Messaging;

namespace SearchBugs.Application.Git.GetPackQuery;

public record GetPackQuery(string RepoName, string PackName) : IQuery<byte[]>;

