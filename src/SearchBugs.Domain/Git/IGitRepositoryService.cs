using SearchBugs.Infrastructure.Services;
using Shared.Results;

namespace SearchBugs.Domain.Git;

public interface IGitRepositoryService
{
    Result CheckoutBranch(string repoPath, string branchName);
    Result CommitChanges(string repoPath, string authorName, string authorEmail, string commitMessage);
    Result<IEnumerable<FileDiff>> CompareCommits(string repoPath, string baseCommitSha, string compareCommitSha);
    Result<IEnumerable<FileDiff>> GetCommitDiff(string repoPath, string commitSha);
    Result<IEnumerable<Contributor>> GetContributors(string repoPath);
    Result<IEnumerable<FileBlame>> GetFileBlame(string repoPath, string filePath);
    Result<string> GetFileContent(string repoPath, string commitSha, string filePath);
    Result<IEnumerable<GitTreeItem>> ListTree(string commitSha, string repoPath);
    Result<MergeResult> MergeBranches(string repoPath, string sourceBranchName, string targetBranchName, string mergerName, string mergerEmail);
}