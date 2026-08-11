using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ProfessionalScheduleConfiguration : IEntityTypeConfiguration<ProfessionalSchedule>
    {
        public void Configure(EntityTypeBuilder<ProfessionalSchedule> builder)
        {
            builder.ToTable("professional_schedules");
            builder.HasKey(ps => ps.IdProfessionalSchedule);
            builder.Property(ps => ps.IdTenant).IsRequired();
            builder.Property(ps => ps.IdProfessional).IsRequired();
            builder.Property(ps => ps.DayOfWeek).IsRequired();
            builder.Property(ps => ps.StartTime).IsRequired();
            builder.Property(ps => ps.EndTime).IsRequired();
            builder.Property(ps => ps.IsActive).IsRequired();
            builder.Property(ps => ps.CreatedAt).IsRequired();
            builder.Property(ps => ps.UpdatedAt);

            builder.HasOne(ps => ps.Tenant)
                .WithMany(t => t.ProfessionalSchedules)
                .HasForeignKey(ps => ps.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ps => ps.Professional)
                .WithMany(p => p.ProfessionalSchedules)
                .HasForeignKey(ps => new { ps.IdTenant, ps.IdProfessional })
                .HasPrincipalKey(p => new { p.IdTenant, p.IdProfessional })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ps => new
            {
                ps.IdTenant,
                ps.IdProfessional,
                ps.DayOfWeek,
                ps.StartTime,
                ps.EndTime
            }).IsUnique();
        }
    }
}
