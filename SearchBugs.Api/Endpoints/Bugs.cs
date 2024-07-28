using Carter;
using MediatR;
using SearchBugs.Application.BugTracking.Create;
using SearchBugs.Application.BugTracking.GetBugs;

namespace SearchBugs.Api.Endpoints;

public sealed class Bugs : ICarterModule
{
    record CreateBugRequest(
        string Title,
        string Description,
        string Status,
        string Priority,
        string Severity,
        Guid ProjectId,
        Guid AssigneeId,
        Guid ReporterId);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/bugs", async (ISender sender) =>
        {
            var query = new GetBugsQuery(Guid.NewGuid());
            var result = await sender.Send(query);
            return Results.Ok(result);
        });


        app.MapPost("/bugs", async (
            CreateBugRequest request,
            ISender sender) =>
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
        }).RequireAuthorization();



    }
}
