using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TenantSubscriptionStatusConfiguration
        : IEntityTypeConfiguration<TenantSubscriptionStatus>
    {
        public void Configure(
            EntityTypeBuilder<TenantSubscriptionStatus> builder)
        {
            builder.ToTable("tenant_subscription_statuses");

            builder.HasKey(s => s.IdTenantSubscriptionStatus);

            builder.Property(s => s.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Description)
                .HasMaxLength(255);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.UpdatedAt);

            builder.HasIndex(s => s.Code)
                .IsUnique();

            builder.HasData(
                new TenantSubscriptionStatus
                {
                    IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.PENDING,
                    Code = TenantSubscriptionStatusCode.PENDING,
                    Name = "Pendiente",
                    Description = "La suscripción está pendiente de activación.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantSubscriptionStatus
                {
                    IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.ACTIVE,
                    Code = TenantSubscriptionStatusCode.ACTIVE,
                    Name = "Activa",
                    Description = "La suscripción se encuentra activa.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantSubscriptionStatus
                {
                    IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.SCHEDULED,
                    Code = TenantSubscriptionStatusCode.SCHEDULED,
                    Name = "Programada",
                    Description = "La suscripción ha sido programada para comenzar en una fecha futura.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantSubscriptionStatus
                {
                    IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.CANCELLED,
                    Code = TenantSubscriptionStatusCode.CANCELLED,
                    Name = "Cancelada",
                    Description = "La suscripción ha sido cancelada.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantSubscriptionStatus
                {
                    IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.EXPIRED,
                    Code = TenantSubscriptionStatusCode.EXPIRED,
                    Name = "Expirada",
                    Description = "La suscripción ha expirado.",
                    CreatedAt = SeedConstants.SeedDate
                },
                new TenantSubscriptionStatus
                {
                    IdTenantSubscriptionStatus = (short)TenantSubscriptionStatusEnum.SUSPENDED,
                    Code = TenantSubscriptionStatusCode.SUSPENDED,
                    Name = "Suspendida",
                    Description = "La suscripción ha sido suspendida temporalmente.",
                    CreatedAt = SeedConstants.SeedDate
                }
);
        }
    }
}