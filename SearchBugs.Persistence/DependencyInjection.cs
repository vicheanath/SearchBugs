using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SearchBugs.Domain;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Repositories;
using Shared.Data;
using Shared.Options;

namespace SearchBugs.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            ConnectionStringOptions connectionStringOptions = serviceProvider.GetService<IOptions<ConnectionStringOptions>>()!.Value;
            options
                .UseNpgsql(connectionStringOptions,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    })
            .UseSnakeCaseNamingConvention();
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IBugRepository, BugRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddMemoryCache()
            .ConfigureOptions<ConnectionStringSetup>();
        services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddTransient<ISqlQueryExecutor, SqlQueryExecutor>();
        return services;
    }
}
