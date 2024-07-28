using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class CustomFieldConfiguration : IEntityTypeConfiguration<CustomField>
{
    public void Configure(EntityTypeBuilder<CustomField> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<CustomField> builder)
    {
        builder.ToTable(TableNames.CustomFields);
        builder.HasKey(cf => cf.Id);
        builder.Property(cf => cf.Id)
            .HasConversion(id => id.Value, value => new CustomFieldId(value))
            .ValueGeneratedNever();
        builder.Property(cf => cf.Name)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(cf => cf.FieldType)
            .HasMaxLength(200)
            .IsRequired();
    }

    private static void ConfigureRelationships(EntityTypeBuilder<CustomField> builder)
    {
        builder.HasMany<BugCustomField>()
            .WithOne()
            .HasForeignKey(cfv => cfv.CustomFieldId);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<CustomField> builder)
    {
        builder.HasIndex(cf => cf.Name);
    }


}
