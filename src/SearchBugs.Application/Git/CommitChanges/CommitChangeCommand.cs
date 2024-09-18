using Shared.Messaging;

namespace SearchBugs.Application.Git.CommitChanges;

public record CommitChangeCommand(string Url, string AuthorName, string AuthorEmail, string CommitMessage, string FileContent) : ICommand;
