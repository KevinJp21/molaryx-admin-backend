using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PaymentFrequencyConfiguration : IEntityTypeConfiguration<PaymentFrequency>
    {
        public void Configure(EntityTypeBuilder<PaymentFrequency> builder)
        {
            builder.ToTable("payment_frequencies");

            builder.HasKey(p => p.IdPaymentFrequency);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.Property(p => p.IsActive).IsRequired();

            builder.HasData(
                new PaymentFrequency
                {
                    IdPaymentFrequency = (short)PaymentFrequencyEnum.ONE_TIME,
                    Name = "Pago único",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PaymentFrequency
                {
                    IdPaymentFrequency = (short)PaymentFrequencyEnum.WEEKLY,
                    Name = "Semanal",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PaymentFrequency
                {
                    IdPaymentFrequency = (short)PaymentFrequencyEnum.BIWEEKLY,
                    Name = "Quincenal",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new PaymentFrequency
                {
                    IdPaymentFrequency = (short)PaymentFrequencyEnum.MONTHLY,
                    Name = "Mensual",
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}
