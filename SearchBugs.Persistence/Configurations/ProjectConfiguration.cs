using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Projects;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder) =>

        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable(TableNames.Projects);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => new ProjectId(value))
            .ValueGeneratedNever();
        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(p => p.Description)
            .HasMaxLength(2000);
        builder.Property(client => client.CreatedOnUtc).IsRequired();
        builder.Property(client => client.ModifiedOnUtc).IsRequired(false);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Project> builder)
    {

    }

    private static void ConfigureIndexes(EntityTypeBuilder<Project> builder)
    {
        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}
