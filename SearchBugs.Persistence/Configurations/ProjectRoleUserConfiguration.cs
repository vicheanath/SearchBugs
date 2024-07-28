using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;

namespace SearchBugs.Persistence.Configurations;

internal sealed class ProjectRoleUserConfiguration : IEntityTypeConfiguration<ProjectRoleUser>
{
    public void Configure(EntityTypeBuilder<ProjectRoleUser> builder)
    {
        builder.ToTable(TableNames.ProjectRoleUsers);
        builder.HasKey(pru => new { pru.ProjectId, pru.UserId, pru.RoleId });
        builder.Property(pru => pru.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));
        builder.Property(pru => pru.ProjectId)
            .HasConversion(id => id.Value, value => new ProjectId(value));
        builder.Property(pru => pru.RoleId);
    }
}
