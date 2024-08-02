
using SearchBugs.Api.Endpoints;
using SearchBugs.Api.Middleware;
using SearchBugs.Application;
using SearchBugs.Infrastructure;
using SearchBugs.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrations();
}
app.MapAuthenticationsEndpoints();
app.MapBugsEndpoints();
app.MapUserEndpoints();
app.MapProjectsEndpoints();
app.MapGitEndpoints();

app.UseHttpsRedirection();
app.UseAuthentication();

//app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();

public partial class Program { }