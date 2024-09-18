using Shared.Messaging;

namespace SearchBugs.Application.Git.GetListTree;

public record GetListTreeQuery(string Url, string CommitSha) : IQuery<IEnumerable<GitTreeItemResult>>;
