using Domain.Entities;
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

            builder.Property(ts => ts.StartedAt).IsRequired();

            builder.Property(ts => ts.ExpiresAt);

            builder.Property(ts => ts.CreatedAt).IsRequired();

            builder.Property(ts => ts.UpdatedAt);

            builder.HasOne(ts => ts.TenantSubscriptionStatuses)
                .WithMany(s => s.TenantSubscriptions)
                .HasForeignKey(ts => ts.IdTenantSubscriptionStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.Tenants)
                .WithMany(t => t.TenantSubscriptions)
                .HasForeignKey(ts => ts.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.Plans)
                .WithMany(p => p.TenantSubscriptions)
                .HasForeignKey(ts => ts.IdPlan)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ts => ts.IdTenant);

            builder.HasIndex(ts => ts.IdPlan);
        }
    }
}