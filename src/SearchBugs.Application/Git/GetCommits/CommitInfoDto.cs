namespace SearchBugs.Application.Git.GetCommits;

public record CommitInfoDto(string Sha, string Message, string AuthorName, string AuthorEmail, DateTime When);
