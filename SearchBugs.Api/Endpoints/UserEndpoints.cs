using MediatR;
using SearchBugs.Application.Users.GetUserDetail;
using SearchBugs.Application.Users.GetUsers;
using SearchBugs.Application.Users.UpdateUser;

namespace SearchBugs.Api.Endpoints;

public static class UserEndpoints
{

    public record UpdateUserRequest(string Name);

    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var users = app.MapGroup("api/users");
        users.MapGet("", GetUsers).WithName(nameof(GetUsers));
        users.MapGet("{id}", GetUserDetail).WithName(nameof(GetUserDetail));
        users.MapPut("{id}", UpdateUser).WithName(nameof(UpdateUser));
    }

    public static async Task<IResult> UpdateUser(
        Guid id,
        UpdateUserRequest request,
        ISender sender)
    {
        var command = new UpdateUserCommand(id, request.Name);
        var result = await sender.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetUsers(ISender sender)
    {
        var query = new GetUsersQuery();
        var result = await sender.Send(query);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetUserDetail(
        Guid id,
        ISender sender)
    {
        var query = new GetUserDetailQuery(id);
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
}
