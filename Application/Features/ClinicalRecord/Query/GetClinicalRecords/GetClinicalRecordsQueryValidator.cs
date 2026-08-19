using FluentValidation;

namespace Application.Features.ClinicalRecord.Query.GetClinicalRecords
{
    public class GetClinicalRecordsQueryValidator : AbstractValidator<GetClinicalRecordsQuery>
    {
        public GetClinicalRecordsQueryValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

            When(x => x.IdAppointment.HasValue, () =>
            {
                RuleFor(x => x.IdAppointment)
                    .GreaterThan(0)
                    .WithMessage("La cita no es válida.");
            });

            When(x => x.IdPatientTreatment.HasValue, () =>
            {
                RuleFor(x => x.IdPatientTreatment)
                    .GreaterThan(0)
                    .WithMessage("El tratamiento del paciente no es válido.");
            });
        }
    }
}
