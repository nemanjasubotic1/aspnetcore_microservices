using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingService.Domain.Models;

namespace Services.OrderingService.OrderingService.API.Infrastructure.Data.Configurations;
public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.HasKey(l => l.Id);

        builder.HasOne(l => l.OrderHeader).WithMany(l => l.OrderDetails).HasForeignKey(l => l.OrderHeaderId);

        builder.Property(l => l.ProductName).IsRequired();
        builder.Property(l => l.Quantity).IsRequired();
        builder.Property(l => l.Price).IsRequired();
    }
}
