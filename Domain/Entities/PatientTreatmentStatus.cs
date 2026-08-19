using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class PatientTreatmentStatus : BaseEntity
    {
        [Column("id_patient_treatment_status")]
        public short IdPatientTreatmentStatus { get; set; }
        
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; }

        public ICollection<PatientTreatment> PatientTreatments { get; set; } = [];
    }
}
