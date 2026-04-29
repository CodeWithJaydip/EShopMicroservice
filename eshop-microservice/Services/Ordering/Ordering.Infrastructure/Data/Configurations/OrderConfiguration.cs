using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<OrderItem>()
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ComplexProperty(o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value).HasColumnName(nameof(Order.OrderName)).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(200).IsRequired();
                addressBuilder.Property(a => a.City).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.State).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(20).IsRequired();
                addressBuilder.Property(a => a.Country).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(200).IsRequired();
                addressBuilder.Property(a => a.City).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.State).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(20).IsRequired();
                addressBuilder.Property(a => a.Country).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardNumber).HasMaxLength(50).IsRequired();
                paymentBuilder.Property(p => p.CardName).HasMaxLength(50);
                paymentBuilder.Property(p => p.Expiration).HasMaxLength(10);
                paymentBuilder.Property(p => p.CVV).HasMaxLength(3);
                paymentBuilder.Property(p => p.PaymentMethod);
            });

            builder.Property(o => o.TotalPrice);
        }
    }
}
