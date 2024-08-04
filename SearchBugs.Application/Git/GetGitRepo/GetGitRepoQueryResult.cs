namespace SearchBugs.Application.Git.GetGitRepo;

record class GetGitRepoQueryResult(
    Guid Id,
    string Name,
    string Description,
    string Url,
    DateTime CreatedOnUtc
    );
