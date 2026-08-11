using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ProfessionalConfiguration : IEntityTypeConfiguration<Professional>
    {
        public void Configure(EntityTypeBuilder<Professional> builder)
        {
            builder.ToTable("professionals");
            builder.HasKey(p => p.IdProfessional);
            builder.Property(p => p.IdTenant).IsRequired();
            builder.Property(p => p.IdUser).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt);

            builder.HasOne(p => p.Tenant)
                .WithMany(t => t.Professionals)
                .HasForeignKey(p => p.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithOne(u => u.Professional)
                .HasForeignKey<Professional>(p => p.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => new { p.IdTenant, p.IdProfessional }).IsUnique();
        }
    }
}