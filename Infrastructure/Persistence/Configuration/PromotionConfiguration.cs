using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("promotions");

            builder.HasKey(p => p.IdPromotion);

            builder.Property(p => p.Code).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);

            builder.Property(p => p.Description);

            builder.Property(p => p.StartsAt).IsRequired();

            builder.Property(p => p.EndsAt);

            builder.Property(p => p.DurationMonths).IsRequired();

            builder.Property(p => p.IsActive).IsRequired();

            builder.Property(p => p.CreatedAt).IsRequired();

            builder.Property(p => p.UpdatedAt);
        }
    }
}