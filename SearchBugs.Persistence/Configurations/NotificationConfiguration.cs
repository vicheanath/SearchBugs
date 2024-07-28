using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Notifications;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(TableNames.Notifications);
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new NotificationId(value));
        builder.Property(n => n.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));
        builder.Property(n => n.Type)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(n => n.Message)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(n => n.BugId)
            .HasConversion(id => id.Value, value => new BugId(value));
        builder.Property(n => n.IsRead)
            .IsRequired();
        builder.Property(n => n.CreatedAt)
            .IsRequired();
    }
    private static void ConfigureRelationships(EntityTypeBuilder<Notification> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.UserId);
        builder.HasOne<Bug>()
            .WithMany()
            .HasForeignKey(n => n.BugId);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Notification> builder)
    {
        builder.HasIndex(n => n.UserId);
    }


}
