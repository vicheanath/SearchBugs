using Shared.Messaging;

namespace SearchBugs.Application.Git.UploadPackQuery;

public record UploadPackCommand(string RepoName, Stream OutPut) : ICommand;

