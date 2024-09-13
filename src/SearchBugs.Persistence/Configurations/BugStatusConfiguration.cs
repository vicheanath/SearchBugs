using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Persistence.Constants;

namespace SearchBugs.Persistence.Configurations;

internal sealed class BugStatusConfiguration : IEntityTypeConfiguration<BugStatus>
{
    public void Configure(EntityTypeBuilder<BugStatus> builder)
    {
        builder.ToTable(TableNames.BugStatus);
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id);
        builder.Property(b => b.Name);

        builder.HasData(BugStatus.GetValues());
    }
}
