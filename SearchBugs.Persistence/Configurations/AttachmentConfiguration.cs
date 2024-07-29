using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable(TableNames.Attachments);
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasConversion(id => id.Value, value => new AttachmentId(value))
            .ValueGeneratedNever();
        builder.Property(a => a.BugId)
            .HasConversion(id => id.Value, value => new BugId(value))
            .IsRequired();
        builder.Property(a => a.FileName)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(a => a.ContentType)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(a => a.Content);
        builder.Property(a => a.CreatedOnUtc)
            .IsRequired();
        builder.Property(a => a.ModifiedOnUtc)
            .IsRequired(false);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasOne<Bug>()
            .WithMany(b => b.Attachments);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasIndex(a => a.BugId);
    }
}
