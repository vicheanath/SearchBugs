using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class BugConfiguration : IEntityTypeConfiguration<Bug>
{
    public void Configure(EntityTypeBuilder<Bug> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Bug> builder)
    {
        builder.ToTable(TableNames.Bugs);
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasConversion(id => id.Value, value => new BugId(value))
            .ValueGeneratedNever();
        builder.Property(b => b.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.Description)
            .HasMaxLength(2000);
        builder.Property(b => b.Severity)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.CreatedOnUtc)
            .IsRequired();
        builder.Property(b => b.ModifiedOnUtc)
            .IsRequired(false);

    }

    private static void ConfigureRelationships(EntityTypeBuilder<Bug> builder)
    {
        //builder.HasOne(b => b.Project)
        //    .WithMany(p => p.Bugs)
        //    .HasForeignKey(b => b.ProjectId)
        //    .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(b => b.Status)
            .WithMany()
            .IsRequired();
        builder.HasOne(b => b.Priority)
            .WithMany()
            .IsRequired();
        builder.HasOne(b => b.ProjectId)
            .WithMany()
            .IsRequired();
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Bug> builder)
    {
        builder.HasIndex(b => b.Title)
            .IsUnique();
    }
}
