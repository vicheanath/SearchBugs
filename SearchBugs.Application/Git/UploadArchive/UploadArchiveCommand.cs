using Shared.Messaging;

namespace SearchBugs.Application.Git.UploadArchive;

public record UploadArchiveCommand(string RepoName, Stream Archive) : ICommand;

