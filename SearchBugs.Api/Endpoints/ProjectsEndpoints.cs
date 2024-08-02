using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchBugs.Application.Projects.CreateProject;
using SearchBugs.Application.Projects.GetProjects;

namespace SearchBugs.Api.Endpoints;

public static class ProjectsEndpoints
{
    public record CreateProjectRequest(
        string Name,
        string Description);

    public static void MapProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        var projects = app.MapGroup("api/projects");
        projects.MapPost("", CreateProject).WithName(nameof(CreateProject));
        projects.MapGet("", GetProjects).WithName(nameof(GetProjects));
    }

    public static async Task<IResult> CreateProject(
        [FromBody] CreateProjectRequest request,
        ISender sender)
    {
        var command = new CreateProjectCommand(
            request.Name,
            request.Description
            );

        var result = await sender.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetProjects(ISender sender)
    {
        var query = new GetProjectsQuery();
        var result = await sender.Send(query);
        return Results.Ok(result);
    }

}
