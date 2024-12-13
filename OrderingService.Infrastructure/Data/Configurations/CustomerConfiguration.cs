using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingService.Domain.Models;

namespace OrderingService.Infrastructure.Data.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Username).IsRequired();
        builder.Property(l => l.Name).IsRequired();
        //builder.Property(l => l.LastName).IsRequired();
        builder.Property(l => l.EmailAddress).IsRequired();
    }
}
