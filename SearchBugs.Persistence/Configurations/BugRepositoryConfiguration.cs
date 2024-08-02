using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Repositories;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class BugRepositoryConfiguration : IEntityTypeConfiguration<BugRepo>
{
    public void Configure(EntityTypeBuilder<BugRepo> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<BugRepo> builder)
    {
        builder.ToTable(TableNames.BugRepository);
        builder.HasKey(br => new { br.BugId, br.RepositoryId });
    }

    private static void ConfigureRelationships(EntityTypeBuilder<BugRepo> builder)
    {
        builder.HasOne<Bug>()
            .WithMany()
            .HasForeignKey(br => br.BugId)
            .IsRequired();

        builder.HasOne<Repository>()
            .WithMany()
            .HasForeignKey(br => br.RepositoryId)
            .IsRequired();
    }

    private static void ConfigureIndexes(EntityTypeBuilder<BugRepo> builder)
    {
        builder.HasIndex(br => br.BugId);
        builder.HasIndex(br => br.RepositoryId);
    }



}
