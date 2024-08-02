
using Shared.Messaging;

namespace SearchBugs.Application.Git.GetInfoRefsQuery;

public record InfoRefsQuery(string RepoName) : IQuery<MemoryStream>;
