using Dapper;

namespace Shared.Data;

/// <summary>
/// Represents the SQL query executor.
/// </summary>
internal sealed class SqlQueryExecutor : ISqlQueryExecutor
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

    public async Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().ExecuteScalarAsync<T>(sql, parameters);
}
