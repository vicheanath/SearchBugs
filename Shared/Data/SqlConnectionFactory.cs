using Microsoft.Extensions.Options;
using Npgsql;
using Shared.Options;
using System.Data;

namespace Shared.Data;

/// <summary>
/// Represents the SQL connection factory.
/// </summary>
internal sealed class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
{
    private readonly ConnectionStringOptions _connectionString;
    private IDbConnection? _connection;

    public SqlConnectionFactory(IOptions<ConnectionStringOptions> connectionString) => _connectionString = connectionString.Value;

    /// <inheritdoc />
    public IDbConnection GetOpenConnection()
    {
        if ((_connection ??= new NpgsqlConnection(_connectionString)).State != ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }

    /// <inheritdoc />
    public void Dispose() => _connection?.Dispose();
}
