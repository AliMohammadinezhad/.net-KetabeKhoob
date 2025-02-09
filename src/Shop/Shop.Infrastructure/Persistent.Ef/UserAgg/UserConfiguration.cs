using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.UserAgg;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Aggregate Root
        builder.ToTable("Users", "user");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AvatarName)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.Family)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Gender)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Password)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11);

        // Value Objects
        // Addresses
        builder.OwnsMany(x => x.Addresses, navigationBuilder =>
        {
            navigationBuilder.ToTable("Addresses", "user");
            navigationBuilder.HasKey(x => x.Id);
            navigationBuilder.HasIndex(x => x.UserId);

            navigationBuilder.Property(x => x.Shire)
                .IsRequired().HasMaxLength(155);

            navigationBuilder.Property(x => x.City)
                .IsRequired().HasMaxLength(125);

            navigationBuilder.Property(x => x.Family)
                .IsRequired().HasMaxLength(125);

            navigationBuilder.Property(x => x.NationalCode)
                .IsRequired().HasMaxLength(10);

            navigationBuilder.Property(x => x.PostalCode)
                .IsRequired().HasMaxLength(20);

            navigationBuilder.OwnsOne(x => x.PhoneNumber, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(x => x.Value)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("PhoneNumber");
            });
        });

        // UserRoles
        builder.OwnsMany(x => x.UserRoles, navigationBuilder =>
        {
            navigationBuilder.ToTable("UserRoles", "user");
            navigationBuilder.HasKey(x => x.Id);
            navigationBuilder.HasIndex(x => x.UserId);
            navigationBuilder.HasIndex(x => x.RoleId);
        });

        // Wallets
        builder.OwnsMany(x => x.Wallets, navigationBuilder =>
        {
            navigationBuilder.ToTable("Wallets", "user");
            navigationBuilder.HasKey(x => x.Id);
            navigationBuilder.HasIndex(x => x.UserId);

            navigationBuilder.Property(x => x.Description)
                .IsRequired().HasMaxLength(512);

            navigationBuilder.Property(x => x.IsFinally)
                .IsRequired();

            navigationBuilder.Property(x => x.Price)
                .IsRequired()
                .HasMaxLength(2048);

            navigationBuilder.Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>();
        });


        // UserToken
        builder.OwnsMany(x => x.Tokens, navigationBuilder =>
        {
            navigationBuilder.ToTable("Tokens", "user");
            navigationBuilder.HasKey(x => x.Id);

            navigationBuilder.Property(x => x.HashJwtToken)
                .IsRequired()
                .HasMaxLength(2048);

            navigationBuilder.Property(x => x.HashRefreshToken)
                .IsRequired()
                .HasMaxLength(2048);

            navigationBuilder.Property(x => x.Device)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}