using Shared.Messaging;

namespace SearchBugs.Application.Git.GetFile;

public record GetFileQuery(string RepoName, string FileName) : IQuery<byte[]>;

