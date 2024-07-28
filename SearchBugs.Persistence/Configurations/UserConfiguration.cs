using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder) =>
          builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => new UserId(value))
            .ValueGeneratedNever();
        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            name.Property(n => n.LastName)
                .HasMaxLength(100)
                .IsRequired();
        });
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                .HasMaxLength(200)
                .IsRequired();
        });
        builder.Property(u => u.Password)
            .HasMaxLength(200)
            .IsRequired();
    }

    private static void ConfigureRelationships(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable(TableNames.UserRoles));
    }

    private static void ConfigureIndexes(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email.Value)
            .IsUnique();
    }
}
