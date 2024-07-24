using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.Options;

/// <summary>
/// Represents the <see cref="ConnectionStringOptions"/> setup.
/// </summary>
internal sealed class ConnectionStringSetup : IConfigureOptions<ConnectionStringOptions>
{
    private const string ConnectionStringName = "Database";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionStringSetup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public ConnectionStringSetup(IConfiguration configuration) => _configuration = configuration;

    /// <inheritdoc />
#pragma warning disable CS8601 // Possible null reference assignment.
    public void Configure(ConnectionStringOptions options) => options.Value = _configuration.GetConnectionString(ConnectionStringName);
#pragma warning restore CS8601 // Possible null reference assignment.
}
