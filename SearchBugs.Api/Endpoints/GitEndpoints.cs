using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchBugs.Application.Git.CreateGitRepo;
using SearchBugs.Application.Git.DeleteGitRepo;
using SearchBugs.Application.Git.GetFile;
using SearchBugs.Application.Git.GetHeadQuery;
using SearchBugs.Application.Git.GetInfoRefsQuery;
using SearchBugs.Application.Git.GetObjectQuery;
using SearchBugs.Application.Git.GetPackIndexQuery;
using SearchBugs.Application.Git.GetPackQuery;
using SearchBugs.Application.Git.GetPacksQuery;
using SearchBugs.Application.Git.ReceivePack;
using SearchBugs.Application.Git.UploadArchive;
using SearchBugs.Application.Git.UploadPackQuery;

namespace SearchBugs.Api.Endpoints;

public static class GitEndpoints
{
    public static void MapGitEndpoints(this WebApplication app)
    {
        var git = app.MapGroup("git");
        git.MapGet("{repoName}/info/refs", InfoRefs).WithName(nameof(InfoRefs));
        git.MapGet("{repoName}/HEAD", Head).WithName(nameof(Head));
        git.MapGet("{repoName}/objects/{objectName}", GetObject).WithName(nameof(GetObject));
        git.MapGet("{repoName}/objects/info/packs", GetPacks).WithName(nameof(GetPacks));
        git.MapGet("{repoName}/objects/pack/pack-{packName}.pack", GetPack).WithName(nameof(GetPack));
        git.MapGet("{repoName}/objects/pack/pack-{packName}.idx", GetPackIndex).WithName(nameof(GetPackIndex));

        git.MapPost("{repoName}/git-upload-pack", UploadPack).WithName(nameof(UploadPack));
        git.MapPost("{repoName}/git-receive-pack", ReceivePack).WithName(nameof(ReceivePack));
        git.MapPost("{repoName}/git-upload-archive", UploadArchive).WithName(nameof(UploadArchive));

        var gitApi = app.MapGroup("api/git");
        gitApi.MapGet("{repoName}/file/{filePath}", GetFile).WithName(nameof(GetFile));
        gitApi.MapPost("create", CreateRepository).WithName(nameof(CreateRepository));
        gitApi.MapDelete("{url}", DeleteRepository).WithName(nameof(DeleteRepository));
    }

    public record CreateGitRepositoryRequest(string Name, string Description, string Url, Guid ProjectId);



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

    public static async Task<IResult> InfoRefs(string repoName, [FromQuery] string service, ISender sender, HttpResponse response)
    {
        var result = await sender.Send(new InfoRefsQuery(repoName));
        return Results.Stream(result.Value, "application/x-git-{service}-advertisement");
    }

    public static async Task<IResult> Head(string repoName, ISender sender)
    {
        var result = await sender.Send(new HeadQuery(repoName));

        return Results.File(result.Value, "text/plain");
    }

    public static async Task<IResult> GetObject(string repoName, string objectName, ISender sender)
    {
        var result = await sender.Send(new GetObjectQuery(repoName, objectName));

        return Results.File(result.Value, "application/x-git-loose-object");
    }

    public static async Task<IResult> GetPacks(string repoName, ISender sender)
    {
        var result = await sender.Send(new GetPacksQuery(repoName));

        return Results.File(result.Value, "text/plain");
    }

    public static async Task<IResult> GetPack(string repoName, string packName, ISender sender)
    {
        var result = await sender.Send(new GetPackQuery(repoName, packName));

        return Results.File(result.Value, "application/x-git-packed-objects");
    }

    public static async Task<IResult> GetPackIndex(string repoName, string packName, ISender sender)
    {
        var result = await sender.Send(new GetPackIndexQuery(repoName, packName));

        return Results.File(result.Value, "application/x-git-packed-objects-toc");
    }

    public static async Task<IResult> UploadPack(HttpRequest request, string repoName, ISender sender)
    {
        var result = await sender.Send(new UploadPackCommand(repoName, request.Body));

        return Results.Ok();
    }

    public static async Task<IResult> ReceivePack(HttpRequest request, string repoName, ISender sender)
    {
        await sender.Send(new ReceivePackCommand(repoName, request.Body));

        return Results.Ok();
    }

    public static async Task<IResult> UploadArchive(HttpRequest request, string repoName, ISender sender)
    {
        await sender.Send(new UploadArchiveCommand(repoName, request.Body));

        return Results.Ok();
    }

    public static async Task<IResult> GetFile(string repoName, string filePath, ISender sender)
    {
        var result = await sender.Send(new GetFileQuery(repoName, filePath));

        return Results.File(result.Value, "application/octet-stream");
    }
}
