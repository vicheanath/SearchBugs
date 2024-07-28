using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(TableNames.Comments);
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, value => new CommentId(value))
            .ValueGeneratedNever();
        builder.Property(c => c.BugId)
            .HasConversion(id => id.Value, value => new BugId(value));
        builder.Property(c => c.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));
        builder.Property(c => c.CommentText)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(c => c.CreatedOnUtc)
            .IsRequired();
        builder.Property(c => c.ModifiedOnUtc)
            .IsRequired(false);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne<Bug>()
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.BugId);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Comment> builder)
    {
        builder.HasIndex(c => c.BugId);
    }

}
