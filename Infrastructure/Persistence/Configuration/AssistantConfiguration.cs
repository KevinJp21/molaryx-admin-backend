using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class AssistantConfiguration : IEntityTypeConfiguration<Assistant>
    {
        public void Configure(EntityTypeBuilder<Assistant> builder)
        {
            builder.ToTable("assistants");
            builder.HasKey(a => a.IdAssistant);
            builder.Property(a => a.IdTenant).IsRequired();
            builder.Property(a => a.IdUser).IsRequired();

            builder.HasOne(a => a.Tenant)
                .WithMany(t => t.Assistants)
                .HasForeignKey(a => a.IdTenant)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                .WithOne(u => u.Assistant)
                .HasForeignKey<Assistant>(a => a.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => new { a.IdTenant, a.IdAssistant }).IsUnique();
        }
    }
}