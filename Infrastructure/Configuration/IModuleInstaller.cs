using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

/// <summary>
/// Represents the interface for installing a module.
/// </summary>
public interface IModuleInstaller
{
    /// <summary>
    /// Installs the module using the specified service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    public void Install(IServiceCollection services, IConfiguration configuration);
}
