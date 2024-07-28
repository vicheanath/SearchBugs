using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;

namespace SearchBugs.Persistence.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Roles);

        builder.HasData(Role.GetValues());
    }
}
