using Carter;
using MediatR;
using SearchBugs.Application.Authentications.Login;
using SearchBugs.Application.Authentications.Register;

namespace SearchBugs.Api.Endpoints;

public class Authentications : ICarterModule
{
    record LoginRequest(string Email, string Password);

    record RefreshTokenRequest(string Token);

    record RegisterRequest(string Email, string Password, string FirstName, string LastName);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var auth = app.MapGroup("/auth");
        auth.MapPost("/login", async (LoginRequest req, ISender sender) =>
        {
            var command = new LoginCommand(req.Email, req.Password);
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
        auth.MapPost("/refresh-token", async (RefreshTokenRequest req, ISender sender) =>
        {
            //var command = new RefreshTokenCommand(req.Token);
            //var result = await sender.Send(command);
            //return Results.Ok(result);
        });

        auth.MapPost("/register", async (RegisterRequest req, ISender sender) =>
        {
            var command = new RegisterCommand(req.Email, req.Password, req.FirstName, req.LastName);
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
    }
}
