using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CommentAgg;

namespace Shop.Infrastructure.Persistent.Ef.CommentAgg;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Aggregate Root
        builder.ToTable("Comments", "dbo");
        builder.HasKey(c => c.Id);

        builder.Property(x => x.Text)
            .HasMaxLength(2045)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}