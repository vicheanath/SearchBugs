using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class BugCustomFieldConfiguration : IEntityTypeConfiguration<BugCustomField>
{
    public void Configure(EntityTypeBuilder<BugCustomField> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<BugCustomField> builder)
    {
        builder.ToTable(TableNames.BugCustomFields);
        builder.HasKey(cf => cf.Id);
        builder.Property(cf => cf.Id)
            .HasConversion(id => id.Value, value => new BugCustomFieldId(value))
            .ValueGeneratedNever();
        builder.Property(cf => cf.BugId)
            .HasConversion(id => id.Value, value => new BugId(value));
        builder.Property(cf => cf.CustomFieldId)
            .HasConversion(id => id.Value, value => new CustomFieldId(value));
        builder.Property(cf => cf.Value)
            .HasMaxLength(2000);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<BugCustomField> builder)
    {
        builder.HasOne(b => b.Bug)
            .WithMany(bcf => bcf.BugCustomFields)
            .HasForeignKey(bcf => bcf.BugId);
        builder.HasOne(ctf => ctf.CustomField)
            .WithMany(b => b.BugCustomFields)
            .HasForeignKey(bcf => bcf.CustomFieldId);

    }

    private static void ConfigureIndexes(EntityTypeBuilder<BugCustomField> builder)
    {
        builder.HasIndex(cf => cf.BugId);
    }
}
