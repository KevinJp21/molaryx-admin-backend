using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.ToTable("procedures");

            builder.HasKey(p => p.IdProcedure);
            builder.Property(p => p.IdTenant).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description);
            builder.Property(p => p.ReferencePrice).HasPrecision(12, 2);
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasOne(p => p.Tenant)
                .WithMany(t => t.Procedures)
                .HasForeignKey(p => p.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => new { p.IdTenant, p.IdProcedure }).IsUnique();
            builder.HasIndex(p => new { p.IdTenant, p.Name })
                .IsUnique()
                .HasFilter("deleted_at IS NULL");
        }
    }
}
