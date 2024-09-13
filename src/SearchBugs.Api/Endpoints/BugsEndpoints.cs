
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchBugs.Application.BugTracking.Create;
using SearchBugs.Application.BugTracking.GetBugs;

namespace SearchBugs.Api.Endpoints;

public static class BugsEndpoints
{
    public record CreateBugRequest(
        string Title,
        string Description,
        string Status,
        string Priority,
        string Severity,
        Guid ProjectId,
        Guid AssigneeId,
        Guid ReporterId);
    public static void MapBugsEndpoints(this IEndpointRouteBuilder app)
    {
        var bugs = app.MapGroup("api/bugs");
        bugs.MapGet("", GetBugs).WithName(nameof(GetBugs));
        bugs.MapPost("", CreateBug).WithName(nameof(CreateBug));
    }

    public static async Task<IResult> GetBugs(ISender sender)
    {
        var query = new GetBugsQuery(Guid.NewGuid());
        var result = await sender.Send(query);
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateBug(
           [FromBody] CreateBugRequest request,
            ISender sender)
    {
        var command = new CreateBugCommand(
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            request.Severity,
            request.ProjectId,
            request.AssigneeId,
            request.ReporterId
            );

        var result = await sender.Send(command);
        return Results.Ok(result);
    }
}
