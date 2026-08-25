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
        public DbSet<Promotion> Promotions { get; set; } = null!;
        public DbSet<PromotionPlan> PromotionPlans { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Procedure> Procedures { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<AppointmentProcedure> AppointmentProcedures { get; set; } = null!;

        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; } = null!;
        public DbSet<Professional> Professionals { get; set; } = null!;
        public DbSet<Assistant> Assistants { get; set; } = null!;
        public DbSet<Treatment> Treatments { get; set; } = null!;
        public DbSet<PatientTreatmentStatus> PatientTreatmentStatuses { get; set; } = null!;
        public DbSet<PatientTreatment> PatientTreatments { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public DbSet<PaymentFrequency> PaymentFrequencies { get; set; } = null!;
        public DbSet<ClinicalRecord> ClinicalRecords { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("pg_trgm");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.SeedAuthorization();
        }
    }
}