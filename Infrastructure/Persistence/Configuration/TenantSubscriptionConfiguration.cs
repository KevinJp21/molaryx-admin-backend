using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TenantSubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {
            builder.ToTable("tenant_subscriptions");

            builder.HasKey(ts => ts.IdTenantSubscription);

            builder.Property(ts => ts.IdTenantSubscriptionStatus).IsRequired();

            builder.Property(ts => ts.IdTenant).IsRequired();

            builder.Property(ts => ts.IdPlan).IsRequired();

            builder.Property(ts => ts.Price).IsRequired().HasPrecision(12, 2);

            builder.Property(ts => ts.MaxProfessionals);

            builder.Property(ts => ts.MaxAssistants);

            builder.Property(ts => ts.MaxPatients);

            builder.Property(ts => ts.StartsAt);

            builder.Property(ts => ts.EndsAt);

            builder.Property(ts => ts.CreatedAt).IsRequired();

            builder.Property(ts => ts.UpdatedAt);

            builder.HasOne(ts => ts.TenantSubscriptionStatus)
                .WithMany(s => s.TenantSubscriptions)
                .HasForeignKey(ts => ts.IdTenantSubscriptionStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.Tenant)
                .WithMany(t => t.TenantSubscriptions)
                .HasForeignKey(ts => ts.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.Plan)
                .WithMany(p => p.TenantSubscriptions)
                .HasForeignKey(ts => ts.IdPlan)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ts => ts.IdTenant)
                .IsUnique()
                .HasFilter($"id_tenant_subscription_status = {(short)TenantSubscriptionStatusEnum.ACTIVE}");
        }
    }
}