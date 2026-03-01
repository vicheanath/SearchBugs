using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchBugs.Api.Extensions;
using SearchBugs.Application.Git.CloneRepository;
using SearchBugs.Application.Git.CommitChanges;
using SearchBugs.Application.Git.CreateGitRepo;
using SearchBugs.Application.Git.DeleteGitRepo;
using SearchBugs.Application.Git.GetBranches;
using SearchBugs.Application.Git.GetCommitDiff;
using SearchBugs.Application.Git.GetCommits;
using SearchBugs.Application.Git.GetFileContents;
using SearchBugs.Application.Git.GetGitRepo;
using SearchBugs.Application.Git.GetGitReposDetails;
using SearchBugs.Application.Git.GetListTree;
using SearchBugs.Application.Git.GitHttpServer;
using SearchBugs.Application.Git.CompareCommits;
using SearchBugs.Application.Git.CreatePullRequest;
using SearchBugs.Application.Git.GetPullRequest;
using SearchBugs.Application.Git.GetPullRequests;
using SearchBugs.Application.Git.MergeBranches;
using SearchBugs.Application.Git.MergePullRequest;
using SearchBugs.Application.Git.Pull;
using SearchBugs.Application.Git.Push;


namespace SearchBugs.Api.Endpoints;

public static class RepoEndpoints
{
    public record CreateGitRepositoryRequest(string Name, string Description, string Url, Guid ProjectId);

    public static void MapRepoEndpoints(this WebApplication app)
    {
        app
            .MapMethods("{name}.git/{**path}", new[] { "GET", "POST", "PUT" }, [ApiExplorerSettings(IgnoreApi = true)] async (
        ISender sender,
            HttpContext httpContext,
            string name,
            string? path,
            CancellationToken cancellationToken) =>
            {
                var command = new GitHttpServerCommand(
                 name,
                 path ?? string.Empty,
                httpContext
            );
                await sender.Send(command, cancellationToken);
            }).ExcludeFromDescription();

        var repo = app.MapGroup("api/repo");
        repo.MapGet("", GetRepositories).WithName("GetGitRepositories").RequireAuthorization("ListAllRepositories");
        repo.MapGet("{url}/{path}", GetRepositoryDetails).WithName("GetGitRepositoryDetails").RequireAuthorization("ViewRepositoryDetails");
        repo.MapPost("", CreateRepository).WithName("CreateGitRepository").RequireAuthorization("CreateRepository");
        repo.MapDelete("{url}", DeleteRepository).WithName("DeleteGitRepository").RequireAuthorization("DeleteRepository");
        repo.MapGet("{url}/commit/{commitSha}", GetCommitDiff).WithName("GetGitCommitDiff").RequireAuthorization("ViewRepositoryDetails");
        repo.MapPost("{url}/commit/{commitSha}", CommitChanges).WithName("CommitGitChanges").RequireAuthorization("UpdateRepository");
        repo.MapGet("{url}/tree/{commitSha}", GetTree).WithName("GetGitTree").RequireAuthorization("ViewRepositoryDetails");
        repo.MapGet("{url}/file/{commitSha}/{**filePath}", GetFileContent).WithName("GetGitFileContent").RequireAuthorization("ViewRepositoryDetails");
        repo.MapPost("{url}/clone", CloneRepository).WithName("CloneGitRepository").RequireAuthorization("CreateRepository");
        repo.MapGet("{url}/branches", GetBranches).WithName("GetGitBranches").RequireAuthorization("ViewRepositoryDetails");
        repo.MapPost("{url}/merge", MergeBranches).WithName("MergeBranches").RequireAuthorization("UpdateRepository");
        repo.MapPost("{url}/push", Push).WithName("Push").RequireAuthorization("UpdateRepository");
        repo.MapPost("{url}/pull", Pull).WithName("Pull").RequireAuthorization("UpdateRepository");
        repo.MapGet("{url}/commits", GetCommits).WithName("GetCommits").RequireAuthorization("ViewRepositoryDetails");
        repo.MapGet("{url}/compare", CompareCommits).WithName("CompareCommits").RequireAuthorization("ViewRepositoryDetails");
        repo.MapGet("{url}/pull-requests", GetPullRequests).WithName("GetPullRequests").RequireAuthorization("ViewRepositoryDetails");
        repo.MapGet("{url}/pull-requests/{id:guid}", GetPullRequest).WithName("GetPullRequest").RequireAuthorization("ViewRepositoryDetails");
        repo.MapPost("{url}/pull-requests", CreatePullRequest).WithName("CreatePullRequest").RequireAuthorization("UpdateRepository");
        repo.MapPost("{url}/pull-requests/{id:guid}/merge", MergePullRequest).WithName("MergePullRequest").RequireAuthorization("UpdateRepository");
    }

    public static async Task<IResult> GetCommitDiff(string url, string commitSha, ISender sender)
    {
        var query = new GetCommitDiffQuery(url, commitSha);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public record CommitChangeRequest(string Author, string Email, string Message, string Content);

    public static async Task<IResult> CommitChanges([FromBody] CommitChangeRequest request, string url, string commitSha, ISender sender)
    {
        var command = new CommitChangeCommand(url, request.Author, request.Email, request.Message, request.Content);
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetTree(string url, string commitSha, ISender sender)
    {
        var query = new GetListTreeQuery(url, commitSha);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetRepositoryDetails(string url, string path, ISender sender)
    {
        var query = new GetGitReposDetailsQuery(url, path);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetRepositories(ISender sender)
    {
        var query = new GetGitRepoQuery();
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> CreateRepository([FromBody] CreateGitRepositoryRequest request, ISender sender)
    {
        var command = new CreateGitRepoCommand(request.Name, request.Description, request.Url, request.ProjectId);
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> DeleteRepository(string url, ISender sender)
    {
        var command = new DeleteGitRepoCommand(url);
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetFileContent(string url, string commitSha, string filePath, ISender sender)
    {
        var query = new GetFileContentQuery(url, commitSha, filePath);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public record CloneRepositoryRequest(string TargetPath);

    public static async Task<IResult> CloneRepository(string url, [FromBody] CloneRepositoryRequest request, ISender sender)
    {
        var command = new CloneRepositoryCommand(url, request.TargetPath);
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetBranches(string url, ISender sender)
    {
        var query = new GetBranchesQuery(url);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public record MergeBranchesRequest(string SourceBranch, string TargetBranch, string AuthorName, string AuthorEmail);

    public static async Task<IResult> MergeBranches(string url, [FromBody] MergeBranchesRequest request, ISender sender)
    {
        var command = new MergeBranchesCommand(url, request.SourceBranch, request.TargetBranch, request.AuthorName, request.AuthorEmail);
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public record PushRequest(string BranchName, string? RemoteName);

    public static async Task<IResult> Push(string url, [FromBody] PushRequest request, ISender sender)
    {
        var command = new PushCommand(url, request.BranchName, request.RemoteName ?? "origin");
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public record PullRequest(string BranchName, string AuthorName, string AuthorEmail, string? RemoteName);

    public static async Task<IResult> Pull(string url, [FromBody] PullRequest request, ISender sender)
    {
        var command = new PullCommand(url, request.BranchName, request.AuthorName, request.AuthorEmail, request.RemoteName ?? "origin");
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetCommits(string url, ISender sender, [FromQuery] string? branch = null, [FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var query = new GetCommitsQuery(url, branch, skip, take);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> CompareCommits(string url, ISender sender, [FromQuery] string baseSha, [FromQuery] string compare)
    {
        var query = new CompareCommitsQuery(url, baseSha, compare);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public record CreatePullRequestRequest(string SourceBranch, string TargetBranch, string Title, string Description, Guid CreatedByUserId);

    public static async Task<IResult> CreatePullRequest(string url, [FromBody] CreatePullRequestRequest request, ISender sender)
    {
        var command = new CreatePullRequestCommand(url, request.SourceBranch, request.TargetBranch, request.Title, request.Description, request.CreatedByUserId);
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetPullRequests(string url, ISender sender, [FromQuery] string? status = null)
    {
        var query = new GetPullRequestsQuery(url, status);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public static async Task<IResult> GetPullRequest(string url, Guid id, ISender sender)
    {
        var query = new GetPullRequestQuery(url, id);
        var result = await sender.Send(query);
        return result!.ToHttpResult();
    }

    public record MergePullRequestRequest(string AuthorName, string AuthorEmail, Guid MergedByUserId);

    public static async Task<IResult> MergePullRequest(string url, Guid id, [FromBody] MergePullRequestRequest request, ISender sender)
    {
        var command = new MergePullRequestCommand(url, id, request.AuthorName, request.AuthorEmail, request.MergedByUserId);
        var result = await sender.Send(command);
        return result!.ToHttpResult();
    }
}
