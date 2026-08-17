using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Patient : BaseEntity
    {
        [Column("id_patient")]
        public long IdPatient { get; set; }

        [Column("id_tenant")]
        public long IdTenant { get; set; }

        [Column("id_identification_type")]
        public short IdIdentificationType { get; set; }

        [Column("identification_number")]
        public string IdentificationNumber { get; set; } = string.Empty;

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("second_name")]
        public string? SecondName { get; set; }

        [Column("first_surname")]
        public string FirstSurname { get; set; } = string.Empty;

        [Column("second_surname")]
        public string? SecondSurname { get; set; }

        [Column("birth_date")]
        public DateOnly BirthDate { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public IdentificationType IdentificationType { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; } = [];
        public ICollection<PatientTreatment> PatientTreatments { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];
    }
}