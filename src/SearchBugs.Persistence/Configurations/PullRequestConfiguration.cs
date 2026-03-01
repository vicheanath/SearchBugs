using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Git;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;

namespace SearchBugs.Persistence.Configurations;

internal sealed class PullRequestConfiguration : IEntityTypeConfiguration<PullRequest>
{
    public void Configure(EntityTypeBuilder<PullRequest> builder)
    {
        builder.ToTable(TableNames.PullRequests);
        builder.HasKey(pr => pr.Id);
        builder.Property(pr => pr.Id)
            .HasConversion(id => id.Value, value => new PullRequestId(value))
            .ValueGeneratedNever();
        builder.Property(pr => pr.RepoUrl).HasMaxLength(500).IsRequired();
        builder.Property(pr => pr.SourceBranch).HasMaxLength(255).IsRequired();
        builder.Property(pr => pr.TargetBranch).HasMaxLength(255).IsRequired();
        builder.Property(pr => pr.Title).HasMaxLength(500).IsRequired();
        builder.Property(pr => pr.Description).HasMaxLength(4000);
        builder.Property(pr => pr.Status).HasConversion<int>().IsRequired();
        builder.Property(pr => pr.CreatedBy)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();
        builder.Property(pr => pr.CreatedAtUtc).IsRequired();
        builder.Property(pr => pr.MergedAtUtc).IsRequired(false);
        builder.Property(pr => pr.MergedBy)
            .HasConversion(id => id == null ? (Guid?)null : id.Value, value => value == null ? null : new UserId(value.Value))
            .IsRequired(false);
    }
}
