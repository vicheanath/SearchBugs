using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Roles;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{

    public void Configure(EntityTypeBuilder<Permission> builder) =>
        builder
            .Tap(ConfigureDataStructure);

    private static void ConfigureDataStructure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);
        builder.HasKey(permission => permission.Id);
        builder.Property(permission => permission.Id).ValueGeneratedNever();
        builder.Property(permission => permission.Name).HasMaxLength(100);
        builder.Property(permission => permission.Description).HasMaxLength(500);
        builder.HasData(Permission.GetValues());
    }
}
