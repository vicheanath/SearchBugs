
using MediatR;
using SearchBugs.Application.Authentications.Login;
using SearchBugs.Application.Authentications.Register;

namespace SearchBugs.Api.Endpoints;

public static class AuthenticationsEndpoints
{
    public record LoginRequest(string Email, string Password);

    public record RefreshTokenRequest(string Token);

    public record RegisterRequest(string Email, string Password, string FirstName, string LastName);

    public static void MapAuthenticationsEndpoints(this IEndpointRouteBuilder app)
    {
        var auth = app.MapGroup("api/auth");
        auth.MapPost("login", Login).WithName(nameof(Login));
        auth.MapPost("register", Register).WithName(nameof(Register));
    }


    public static async Task<IResult> Login(LoginRequest req, ISender sender)
    {
        var command = new LoginCommand(req.Email, req.Password);
        var result = await sender.Send(command);
        return Results.Ok(result);
    }
    public static async Task<IResult> Register(RegisterRequest req, ISender sender)
    {
        var command = new RegisterCommand(req.Email, req.Password, req.FirstName, req.LastName);
        var result = await sender.Send(command);
        return Results.Ok(result);
    }
}
