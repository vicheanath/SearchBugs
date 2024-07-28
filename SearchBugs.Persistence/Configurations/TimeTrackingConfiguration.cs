using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class TimeTrackingConfiguration : IEntityTypeConfiguration<TimeTracking>
{
    public void Configure(EntityTypeBuilder<TimeTracking> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<TimeTracking> builder)
    {
        builder.ToTable(TableNames.TimeTracks);
        builder.HasKey(tt => tt.Id);
        builder.Property(tt => tt.Id)
            .HasConversion(id => id.Value, value => new TimeTracingId(value))
            .ValueGeneratedNever();
        builder.Property(tt => tt.BugId)
            .HasConversion(id => id.Value, value => new BugId(value))
            .IsRequired();
        builder.Property(tt => tt.UserId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();
        builder.Property(tt => tt.TimeSpent)
            .IsRequired();
        builder.Property(tt => tt.LoggedAt)
            .IsRequired();
    }

    private static void ConfigureRelationships(EntityTypeBuilder<TimeTracking> builder)
    {
        builder.HasOne<Bug>()
            .WithMany(b => b.TimeTracking)
            .HasForeignKey(tt => tt.BugId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<TimeTracking> builder)
    {
        builder.HasIndex(tt => tt.BugId);
    }

}
