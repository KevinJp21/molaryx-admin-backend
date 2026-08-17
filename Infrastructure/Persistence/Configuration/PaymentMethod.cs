using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("payment_methods");

            builder.HasKey(p => p.IdPaymentMethod);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.Property(p => p.IsActive).IsRequired();

            builder.HasData(
                new PaymentMethod
                {
                    IdPaymentMethod = (short)PaymentMethodEnum.CASH,
                    Name = "Efectivo",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PaymentMethod
                {
                    IdPaymentMethod = (short)PaymentMethodEnum.CARD,
                    Name = "Tarjeta",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PaymentMethod
                {
                    IdPaymentMethod = (short)PaymentMethodEnum.TRANSFER,
                    Name = "Transferencia",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PaymentMethod
                {
                    IdPaymentMethod = (short)PaymentMethodEnum.OTHER,
                    Name = "Otro",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}
