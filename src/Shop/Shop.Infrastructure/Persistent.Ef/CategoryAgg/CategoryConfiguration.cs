using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CategoryAgg;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAgg;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Aggregate Root
        builder.ToTable("Category", "dbo");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(2048)
            .IsUnicode(false);

        builder
            .HasMany(x => x.Childes)
            .WithOne()
            .HasForeignKey(x => x.ParentId);

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
    }
}