using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Slider;

public class SliderConfiguration : IEntityTypeConfiguration<Domain.SiteEntities.Slider>
{
    public void Configure(EntityTypeBuilder<Domain.SiteEntities.Slider> builder)
    {
        builder.ToTable("Sliders", "dbo");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Link)
            .IsRequired();

        builder.Property(x => x.ImageName)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Position)
            .IsRequired()
            .HasConversion(
                p => p.ToString(),
                x => (SliderPosition)Enum.Parse(typeof(SliderPosition),x)
                );
    }
}