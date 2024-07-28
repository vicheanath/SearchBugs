using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SearchBugs.Domain.Users;
using SearchBugs.Infrastructure.Authentication;

namespace SearchBugs.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<LoggingBackgroundJobSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
    }
}
