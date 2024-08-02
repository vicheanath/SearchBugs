using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using SearchBugs.Domain.Repositories;
using SearchBugs.Domain.Services;
using SearchBugs.Domain.Users;
using SearchBugs.Infrastructure.Authentication;
using SearchBugs.Infrastructure.Options;
using SearchBugs.Infrastructure.Services;

namespace SearchBugs.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        //services.AddQuartz(options =>
        //{
        //    options.UseMicrosoftDependencyInjectionJobFactory();
        //});

        //services.AddQuartzHostedService(options =>
        //{
        //    options.WaitForJobsToComplete = true;
        //});

        services.ConfigureOptions<LoggingBackgroundJobSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<GitOptionsSetup>();
        services.AddScoped<IPasswordHashingService, PasswordHashingService>();
        services.AddScoped<IDataEncryptionService, DataEncryptionService>();
        services.AddScoped<IGitService, GitService>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:5173")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials());
        });

    }
}
