using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {   
        // Aggregate Root
        builder.ToTable("Orders", "order");
        builder.HasKey(x => x.Id);

        
        // Value Objects
        builder.OwnsOne(x => x.Discount, propertyBuilder =>
        {
            propertyBuilder.Property(x => x.DiscountTitle)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(x => x.ShippingMethod, propertyBuilder =>
        {
            propertyBuilder.Property(x => x.ShippingType)
                .HasMaxLength(50)
                .IsRequired();
        });

        // Child Entities
        builder.OwnsMany(x => x.Items, navigationBuilder =>
        {
            navigationBuilder.ToTable("Items", "order");
            navigationBuilder.HasKey(x => x.Id);
        });

        builder.OwnsOne(x => x.Address, navigationBuilder =>
        {
            navigationBuilder.ToTable("Addresses", "order");
            navigationBuilder.HasKey(x => x.Id);

            navigationBuilder.Property(x => x.City)
                .HasMaxLength(50)
                .IsRequired();

            navigationBuilder.Property(x => x.PhoneNumber)
                .HasMaxLength(11)
                .IsRequired();

            navigationBuilder.Property(x => x.Family)
                .HasMaxLength(100)
                .IsRequired();

            navigationBuilder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            navigationBuilder.Property(x => x.NationalCode)
                .HasMaxLength(11)
                .IsRequired();

            navigationBuilder.Property(x => x.PostalCode)
                .HasMaxLength(40)
                .IsRequired();

            navigationBuilder.Property(x => x.PostalAddress)
                .HasMaxLength(512)
                .IsRequired();
        });
    }
}