using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchBugs.Application.Git.CommitChanges;
using SearchBugs.Application.Git.CreateGitRepo;
using SearchBugs.Application.Git.DeleteGitRepo;
using SearchBugs.Application.Git.GetCommitDiff;
using SearchBugs.Application.Git.GetGitRepo;
using SearchBugs.Application.Git.GetGitReposDetails;
using SearchBugs.Application.Git.GetListTree;
using SearchBugs.Application.Git.GitHttpServer;

namespace SearchBugs.Api.Endpoints;

public static class RepoEndpoints
{
    public record CreateGitRepositoryRequest(string Name, string Description, string Url, Guid ProjectId);

    public static void MapRepoEndpoints(this WebApplication app)
    {
        app
            .Map("{name}.git/{**path}", async (
                    ISender sender,
                    string name,
                    CancellationToken cancellationToken) =>
            {
                var command = new GitHttpServerCommand(name, cancellationToken);
                await sender.Send(command, cancellationToken);
            });

        var repo = app.MapGroup("api/repo");
        repo.MapGet("", GetRepositories).WithName(nameof(GetRepositories));
        repo.MapGet("{url}/{path}", GetRepositoryDetails).WithName(nameof(GetRepositoryDetails));
        repo.MapPost("", CreateRepository).WithName(nameof(CreateRepository));
        repo.MapDelete("{url}", DeleteRepository).WithName(nameof(DeleteRepository));
        repo.MapGet("{url}/commit/{commitSha}", GetCommitDiff).WithName(nameof(GetCommitDiff));
        repo.MapPost("{url}/commit/{commitSha}", CommitChanges).WithName(nameof(CommitChanges));
        repo.MapGet("{url}/tree/{commitSha}", GetTree).WithName(nameof(GetTree));
    }

    public static async Task<IResult> GetCommitDiff(string url, string commitSha, ISender sender)
    {
        var query = new GetCommitDiffQuery(url, commitSha);
        var result = await sender.Send(query);
        return Results.Ok(result);
    }

    public record CommitChangeRequest(string Author, string Email, string Message, string Content);

    public static async Task<IResult> CommitChanges([FromBody] CommitChangeRequest request, string url, string commitSha, ISender sender)
    {
        var command = new CommitChangeCommand(url, request.Author, request.Email, request.Message, request.Content);
        var result = await sender.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetTree(string url, string commitSha, ISender sender)
    {
        var query = new GetListTreeQuery(url, commitSha);
        var result = await sender.Send(query);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetRepositoryDetails(string url, string path, ISender sender)
    {
        var query = new GetGitReposDetailsQuery(url, path);
        var result = await sender.Send(query);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetRepositories(ISender sender)
    {
        var query = new GetGitRepoQuery();
        var result = await sender.Send(query);

        return Results.Ok(result);
    }

    public static async Task<IResult> CreateRepository([FromBody] CreateGitRepositoryRequest request, ISender sender)
    {
        var command = new CreateGitRepoCommand(request.Name, request.Description, request.Url, request.ProjectId);
        var result = await sender.Send(command);

        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteRepository(string url, ISender sender)
    {
        var command = new DeleteGitRepoCommand(url);
        var result = await sender.Send(command);

        return Results.Ok(result);
    }
}
