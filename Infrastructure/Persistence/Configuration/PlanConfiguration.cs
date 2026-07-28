using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("plans");

            builder.HasKey(p => p.IdPlan);

            builder.Property(p => p.Code).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Description);

            builder.Property(p => p.Price);

            builder.Property(p => p.MaxProfessionals);

            builder.Property(p => p.MaxAssistants);

            builder.Property(p => p.MaxPatients);

            builder.Property(p => p.CreatedAt).IsRequired();

            builder.Property(p => p.UpdatedAt);

            builder.HasIndex(p => p.Code).IsUnique();

            builder.HasData(
                new Plan
                {
                    IdPlan = (short)PlanEnum.BASIC,
                    Code = PlanCodes.BASIC,
                    Name = "Basic",
                    Description = "Plan básico para consultorios pequeños.",
                    Price = 79900,
                    MaxProfessionals = 1,
                    MaxAssistants = 1,
                    MaxPatients = 500,
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new Plan
                {
                    IdPlan = (short)PlanEnum.PROFESSIONAL,
                    Code = PlanCodes.PROFESSIONAL,
                    Name = "Professional",
                    Description = "Plan para consultorios en crecimiento.",
                    Price = 119900,
                    MaxProfessionals = 3,
                    MaxAssistants = 3,
                    MaxPatients = 2000,
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                },
                new Plan
                {
                    IdPlan = (short)PlanEnum.BUSINESS,
                    Code = PlanCodes.BUSINESS,
                    Name = "Business",
                    Description = "Plan para consultorios con equipos y operaciones de mayor escala.",
                    Price = null,
                    MaxProfessionals = null,
                    MaxAssistants = null,
                    MaxPatients = null,
                    IsActive = true,
                    CreatedAt = SeedConstants.SeedDate
                }
            );
        }
    }
}