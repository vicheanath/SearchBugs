namespace SearchBugs.Domain.Git;

public record CommitInfo(string Sha, string Message, string AuthorName, string AuthorEmail, DateTime When);
