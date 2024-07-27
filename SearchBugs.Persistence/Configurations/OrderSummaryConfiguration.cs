using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SearchBugs.Persistence.Configurations;

internal sealed class OrderSummaryConfiguration : IEntityTypeConfiguration<OrderSummary>
{
    public void Configure(EntityTypeBuilder<OrderSummary> builder)
    {
        builder.HasKey(os => os.Id);

        builder.Property(os => os.LineItems).HasColumnType("jsonb");
    }
}
