using Shared.Messaging;

namespace SearchBugs.Application.Git.GetPacksQuery;

public record GetPacksQuery(string RepoName) : IQuery<byte[]>;

