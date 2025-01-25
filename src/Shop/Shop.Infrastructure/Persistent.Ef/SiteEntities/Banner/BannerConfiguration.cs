using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Banner;

public class BannerConfiguration : IEntityTypeConfiguration<Domain.SiteEntities.Banner>
{
    public void Configure(EntityTypeBuilder<Domain.SiteEntities.Banner> builder)
    {
        // Base Entity
        builder.ToTable("Banners", "dbo");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Position)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.ImageName)
            .IsRequired();

        builder.Property(x => x.Link)
            .IsRequired();

    }
}