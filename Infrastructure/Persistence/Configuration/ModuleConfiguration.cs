using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("modules");

            builder.HasKey(m => m.IdModule);

            builder.Property(m => m.Code).IsRequired().HasMaxLength(50);

            builder.Property(m => m.CreatedAt).IsRequired();

            builder.Property(m => m.UpdatedAt);

            builder.HasIndex(m => m.Code).IsUnique();
        }
    }
}