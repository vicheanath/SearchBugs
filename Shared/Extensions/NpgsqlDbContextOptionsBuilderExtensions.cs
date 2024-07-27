using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Shared.Constants;

namespace Shared.Extensions;

/// <summary>
/// Contains extension method for the <see cref="NpgsqlDbContextOptionsBuilder"/> class.
/// </summary>
public static class NpgsqlDbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Configures the migration history table to live in the specified schema.
    /// </summary>
    /// <param name="dbContextOptionsBuilder">The database context options builder.</param>
    /// <param name="schema">The schema.</param>
    /// <returns>The same database context options builder.</returns>
    public static NpgsqlDbContextOptionsBuilder WithMigrationHistoryTableInSchema(
        this NpgsqlDbContextOptionsBuilder dbContextOptionsBuilder,
        string schema) =>
        dbContextOptionsBuilder.MigrationsHistoryTable(TableNames.MigrationHistory, schema);
}
