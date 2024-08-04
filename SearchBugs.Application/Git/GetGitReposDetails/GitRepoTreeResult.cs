namespace SearchBugs.Application.Git.GetGitReposDetails;

public record GitRepoItem(
        string Id,
        string Url,
        DateTime Date,
        string ShortMessageHtmlLink
    );