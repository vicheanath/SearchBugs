using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Options;
using Shared.Extensions;

namespace Persistence;

/// <summary>
/// Represents the persistence service installer.
/// </summary>
internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMemoryCache()
            .ConfigureOptions<ConnectionStringSetup>()
            .AddTransientAsMatchingInterfaces(AssemblyReference.Assembly)
            .Tap(() => Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true);
}
