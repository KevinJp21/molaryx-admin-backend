using Domain.Entities;
using Infrastructure.Persistence.Seeds.Authorization.Modules;
using Infrastructure.Persistence.Seeds.Authorization.Permissions;
using Infrastructure.Persistence.Seeds.Authorization.RolePermissions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds.Authorization
{
    public static class AuthorizationSeedExtensions
    {
        public static void SeedAuthorization(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>()
                .HasData(ModuleSeed.Data);

            modelBuilder.Entity<Permission>()
                .HasData(TenantPermissionSeed.Data);

            modelBuilder.Entity<Permission>()
                .HasData(UserPermissionSeed.Data);

            modelBuilder.Entity<Permission>()
                .HasData(PatientsPermissionSeed.Data);

            modelBuilder.Entity<Permission>()
                .HasData(ServicePermissionSeed.Data);

            modelBuilder.Entity<Permission>()
                .HasData(AppointmentPermissionSeed.Data);

            modelBuilder.Entity<Permission>()
                .HasData(TreatmentPermissionSeed.Data);

            modelBuilder.Entity<Permission>()
                .HasData(PatientTreatmentPermissionSeed.Data);


            // Agregar permissos de cada rol
            modelBuilder.Entity<RolePermission>()
                .HasData(OwnerPermissionSeed.Data);

            modelBuilder.Entity<RolePermission>()
                .HasData(SurperAdminPermissionSeed.Data);
        }
    }
}