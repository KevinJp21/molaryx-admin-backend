using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PromotionPlanConfiguration : IEntityTypeConfiguration<PromotionPlan>
    {
        public void Configure(EntityTypeBuilder<PromotionPlan> builder)
        {
            builder.ToTable("promotion_plans");

            builder.HasKey(p => new
            {
                p.IdPromotion,
                p.IdPlan
            });

            builder.Property(p => p.Price).IsRequired();

            builder.HasOne(pp => pp.Promotion)
                .WithMany(p => p.PromotionPlans)
                .HasForeignKey(p => p.IdPromotion)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pp => pp.Plan)
                .WithMany(p => p.PromotionPlans)
                .HasForeignKey(pp => pp.IdPlan)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}