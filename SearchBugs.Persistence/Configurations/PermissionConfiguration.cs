using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;

namespace SearchBugs.Persistence.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Permissions)
            .UsingEntity<RolePermission>();

        builder.HasData(Permission.GetValues());
    }
}
