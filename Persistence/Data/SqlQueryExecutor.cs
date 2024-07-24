using Application.Data;
using Application.ServiceLifetimes;
using Dapper;

namespace Persistence.Data;

/// <summary>
/// Represents the SQL query executor.
/// </summary>
internal sealed class SqlQueryExecutor : ISqlQueryExecutor, ITransient
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlQueryExecutor"/> class.
    /// </summary>
    /// <param name="sqlConnectionFactory">The SQL connection factory.</param>
    public SqlQueryExecutor(ISqlConnectionFactory sqlConnectionFactory) => _sqlConnectionFactory = sqlConnectionFactory;

    /// <inheritdoc />
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().QueryAsync<T>(sql, parameters);

    /// <inheritdoc />
    public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, TResult>(
        string sql,
        Func<T1, T2, TResult> map,
        object? parameters = default,
        string splitOn = "Id") =>
        await _sqlConnectionFactory.GetOpenConnection().QueryAsync(sql, map, parameters, splitOn: splitOn);

    /// <inheritdoc />
    public async Task<T?> FirstOrDefaultAsync<T>(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().QueryFirstOrDefaultAsync<T>(sql, parameters);

    /// <inheritdoc />
    public async Task ExecuteAsync(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().ExecuteAsync(sql, parameters);

#pragma warning disable CS8603 // Possible null reference return.
    public async Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().ExecuteScalarAsync<T>(sql, parameters);
#pragma warning restore CS8603 // Possible null reference return.
}
