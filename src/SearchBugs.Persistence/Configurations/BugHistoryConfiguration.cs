using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class BugHistoryConfiguration : IEntityTypeConfiguration<BugHistory>
{
    public void Configure(EntityTypeBuilder<BugHistory> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<BugHistory> builder)
    {
        builder.ToTable(TableNames.BugHistories);
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id)
            .HasConversion(id => id.Value, value => new HistoryId(value))
            .ValueGeneratedNever();
        builder.Property(h => h.BugId)
            .HasConversion(id => id.Value, value => new BugId(value));
        builder.Property(h => h.ChangedBy)
            .HasConversion(id => id.Value, value => new UserId(value));
        builder.Property(h => h.FieldChanged)
            .HasMaxLength(200);
        builder.Property(h => h.OldValue)
            .HasMaxLength(2000);
        builder.Property(h => h.NewValue)
            .HasMaxLength(2000);
        builder.Property(h => h.ChangedAtUtc);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<BugHistory> builder)
    {
        builder.HasOne<Bug>()
            .WithMany(b => b.BugHistories)
            .HasForeignKey(h => h.BugId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(h => h.ChangedBy);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<BugHistory> builder)
    {
        builder.HasIndex(h => h.BugId);
    }
}
