using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Persistence.Constants;

namespace SearchBugs.Persistence.Configurations;

internal sealed class BugPriorityConfiguration : IEntityTypeConfiguration<BugPriority>
{
    public void Configure(EntityTypeBuilder<BugPriority> builder)
    {
        builder.ToTable(TableNames.BugPriorities);
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id);
        builder.Property(b => b.Name);
        builder.HasData(BugPriority.GetValues());
    }
}
