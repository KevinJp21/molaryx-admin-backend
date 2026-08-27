using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notifications");

            builder.HasKey(n => n.IdNotification);

            builder.Property(n => n.IdTenant);
            builder.Property(n => n.IdUser).IsRequired();

            builder.Property(n => n.Type)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(n => n.Subject)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(n => n.Body)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(n => n.IsViewed).IsRequired();

            builder.HasIndex(n => new { n.IdTenant, n.IdUser, n.IsViewed });
            builder.HasIndex(n => n.CreatedAt);

            builder.HasOne(n => n.Tenant)
                .WithMany()
                .HasForeignKey(n => n.IdTenant)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
