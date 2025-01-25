using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.RoleAgg;

namespace Shop.Infrastructure.Persistent.Ef.RoleAgg;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Aggregate Root
        builder.ToTable("Roles", "role");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(125);

        builder.OwnsMany(x => x.Permissions, navigationBuilder =>
        {
            navigationBuilder.ToTable("Permissions", "roles");
            navigationBuilder.HasIndex(x => x.RoleId);
            
            navigationBuilder.Property(x => x.Permission)
                .HasConversion<string>();
        });
    }
}