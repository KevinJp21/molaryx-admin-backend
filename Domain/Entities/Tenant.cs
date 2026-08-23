using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Tenant : BaseEntity
    {
        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_tenant_type")]
        public short IdTenantType { get; set; }

        [Column("id_tenant_status")]
        public short IdTenantStatus { get; set; }
        
        [Column("id_identification_type")]
        public short? IdIdentificationType { get; set; }

        [Column("identification_number")]
        public string? IdentificationNumber { get; set; }

        [Column("consultory_name")]
        public string ConsultoryName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        // Navigation properties
        public TenantType TenantType { get; set; } = null!;
        public TenantStatus TenantStatus { get; set; } = null!;
        public IdentificationType IdentificationType { get; set; } = null!;
        public ICollection<TenantSubscription> TenantSubscriptions { get; set; } = [];
        public ICollection<User> Users { get; set; } = [];
        public ICollection<Professional> Professionals { get; set; } = [];
        public ICollection<Patient> Patients { get; set; } = [];
        public ICollection<Procedure> Procedures { get; set; } = [];

        public ICollection<Appointment> Appointments { get; set; } = [];
        public ICollection<Treatment> Treatments { get; set; } = [];
        public ICollection<PatientTreatment> PatientTreatments { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];
        public ICollection<ClinicalRecord> ClinicalRecords { get; set; } = [];
    }
}