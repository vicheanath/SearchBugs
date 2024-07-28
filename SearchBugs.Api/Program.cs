//using Carter;
using SearchBugs.Api.Middleware;
using SearchBugs.Application;
using SearchBugs.Infrastructure;
using SearchBugs.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
//builder.Services.AddCarter();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.MapCarter();

app.Run();

public partial class Program { }