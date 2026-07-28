using Domain.Entities;
using Infrastructure.Persistence.Seeds.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Tenant> Tenants { get; set; } = null!;
        public DbSet<TenantType> TenantTypes { get; set; } = null!;
        public DbSet<TenantStatus> TenantStatuses { get; set; } = null!;
        public DbSet<Plan> Plans { get; set; } = null!;
        public DbSet<TenantSubscription> TenantSubscriptions { get; set; } = null!;
        public DbSet<TenantSubscriptionStatus> TenantSubscriptionStatuses { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public DbSet<UserSession> UserSessions { get; set; } = null!;
        public DbSet<IdentificationType> IdentificationTypes { get; set; } = null!;
        public DbSet<Module> Modules { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.SeedAuthorization();
        }
    }
}