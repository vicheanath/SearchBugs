using Shared.Messaging;

namespace SearchBugs.Application.Git.GetGitRepo;

public record GetGitRepoQuery() : IQuery<List<GetGitRepoQueryResult>>;

