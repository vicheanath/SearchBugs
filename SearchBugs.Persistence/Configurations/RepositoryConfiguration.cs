using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Repositories;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class RepositoryConfiguration : IEntityTypeConfiguration<Repository>
{
    public void Configure(EntityTypeBuilder<Repository> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Repository> builder)
    {
        builder.ToTable(TableNames.Repository);
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
        .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new RepositoryId(value));
        builder.Property(r => r.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(r => r.ProjectId)
            .HasConversion(id => id.Value, value => new ProjectId(value));
        builder.Property(r => r.Url)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(r => r.Description)
            .HasMaxLength(500);
        builder.Property(r => r.CreatedAt)
            .IsRequired();
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Repository> builder)
    {
        builder.HasOne<Project>()
             .WithMany()
             .HasForeignKey(r => r.ProjectId);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Repository> builder)
    {
        builder.HasIndex(r => r.ProjectId);
        builder.HasIndex(r => r.Url).IsUnique();
    }
}
