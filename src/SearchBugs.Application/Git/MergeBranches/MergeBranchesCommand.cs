using Shared.Messaging;

namespace SearchBugs.Application.Git.MergeBranches;

public record MergeBranchesCommand(
    string RepoUrl,
    string SourceBranch,
    string TargetBranch,
    string AuthorName,
    string AuthorEmail) : ICommand;
