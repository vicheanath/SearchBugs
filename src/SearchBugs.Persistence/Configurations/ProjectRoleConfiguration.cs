using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Projects;
using SearchBugs.Persistence.Constants;

namespace SearchBugs.Persistence.Configurations;

internal sealed class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.ToTable(TableNames.ProjectRoles);
        builder.HasKey(pr => pr.Id);
        builder.Property(pr => pr.Id);
        builder.Property(pr => pr.Name);
        builder.HasMany<Project>()
            .WithMany()
            .UsingEntity<ProjectRoleUser>();
        builder.HasData(ProjectRole.GetValues());

    }
}
