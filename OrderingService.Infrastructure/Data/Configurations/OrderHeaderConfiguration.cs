using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingService.Domain;
using OrderingService.Domain.Models;

namespace OrderingService.Infrastructure.Data.Configurations;
public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.CustomerId);

        builder.ComplexProperty(l => l.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(l => l.FirstName)
                 .HasMaxLength(50)
                 .IsRequired();

            addressBuilder.Property(l => l.LastName)
                 .HasMaxLength(50)
                 .IsRequired();

            addressBuilder.Property(l => l.EmailAddress)
                 .HasMaxLength(50);

            addressBuilder.Property(l => l.AddressLine)
               .HasMaxLength(50)
               .IsRequired();

            addressBuilder.Property(l => l.Country)
               .HasMaxLength(50);

            addressBuilder.Property(l => l.State)
               .HasMaxLength(50);

            addressBuilder.Property(l => l.ZipCode)
               .HasMaxLength(50);
        });

        builder.ComplexProperty(
           l => l.Payment, paymentBuilder =>
           {
               paymentBuilder.Property(l => l.CardName)
                   .HasMaxLength(50);

               paymentBuilder.Property(l => l.CardNumber)
                   .HasMaxLength(24);

               paymentBuilder.Property(l => l.Expiration)
                   .HasMaxLength(10);

               paymentBuilder.Property(l => l.CVV)
                   .HasMaxLength(3);
           });


        builder.Property(l => l.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                status => status.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
            );

        builder.Property(l => l.TotalPrice);


    }
}
