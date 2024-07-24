namespace Persistence.Options;

/// <summary>
/// Represents the connection string.
/// </summary>
public sealed class ConnectionStringOptions
{
    /// <summary>
    /// Gets the connection string value.
    /// </summary>
    public string Value { get; internal set; } = string.Empty;

    public static implicit operator string(ConnectionStringOptions connectionString) => connectionString.Value;
}
