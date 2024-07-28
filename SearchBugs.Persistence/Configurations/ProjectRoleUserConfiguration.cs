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
        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(pru => pru.ProjectId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(pru => pru.UserId);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(pru => pru.RoleId);
    }
}
