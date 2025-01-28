using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerAgg;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        // Base Entity
        builder.ToTable("Sellers", "seller");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NationalCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.ShopName)
            .IsRequired()
            .HasMaxLength(255);

        builder.OwnsMany(x => x.Inventories, navigationBuilder =>
        {
            navigationBuilder.ToTable("Inventories", "seller");
            navigationBuilder.HasKey(x => x.Id);
            navigationBuilder.HasIndex(x => x.ProductId);
            navigationBuilder.HasIndex(x => x.SellerId);
        });
    }
}