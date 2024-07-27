using System.Data;

namespace Shared.Data;

/// <summary>
/// Represents the SQL connection factory interface.
/// </summary>
internal interface ISqlConnectionFactory
{
    /// <summary>
    /// Gets the open database connection instance.
    /// </summary>
    /// <returns>The open database connection instance.</returns>
    IDbConnection GetOpenConnection();
}
