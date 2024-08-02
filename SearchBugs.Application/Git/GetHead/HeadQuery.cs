

using Shared.Messaging;

namespace SearchBugs.Application.Git.GetHeadQuery;

public record HeadQuery(string RepoName) : IQuery<byte[]>;