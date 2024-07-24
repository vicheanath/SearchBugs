using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

/// <summary>
/// Represents the interface for installing services.
/// </summary>
public interface IServiceInstaller
{
    /// <summary>
    /// Installs the required services using the specified service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    void Install(IServiceCollection services, IConfiguration configuration);
}
