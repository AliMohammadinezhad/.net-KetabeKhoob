using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Aggregate Root
        builder.ToTable("Products", "product");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug).IsUnique();


        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.ImageName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Slug)
            .IsRequired()
            .IsUnicode(false);


        // Value Object | Seo Data
        builder.OwnsOne(x => x.SeoData, config =>
        {
            config.Property(x => x.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

            config.Property(x => x.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");


            config.Property(x => x.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");


            config.Property(x => x.IndexPage)
                .HasColumnName("IndexPage");


            config.Property(x => x.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");

            config.Property(x => x.Schema)
                .HasColumnName("Schema");
        });

        // Child Entities
        builder.OwnsMany(x => x.Images, config =>
        {
            config.ToTable("Images", "product");
            config.HasKey(x => x.Id);
            config.Property(x => x.ImageName)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(x => x.Specifications, config =>
        {
            config.ToTable("Specifications", "product");
            config.HasKey(x => x.Id);
            config.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(100);

            config.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(100);
        });

    }
}