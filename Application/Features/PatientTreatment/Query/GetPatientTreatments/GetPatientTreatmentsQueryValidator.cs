using Domain.Enums;
using FluentValidation;

namespace Application.Features.PatientTreatment.Query.GetPatientTreatments
{
    public class GetPatientTreatmentsQueryValidator : AbstractValidator<GetPatientTreatmentsQuery>
    {
        public GetPatientTreatmentsQueryValidator()
        {
            When(x => x.IdPatient.HasValue, () =>
            {
                RuleFor(x => x.IdPatient)
                    .GreaterThan(0)
                    .WithMessage("El paciente no es válido.");
            });

            When(x => x.IdPatientTreatmentStatus.HasValue, () =>
            {
                RuleFor(x => x.IdPatientTreatmentStatus)
                    .Must(status => Enum.IsDefined((PatientTreatmentStatusEnum)status!.Value))
                    .WithMessage("El estado del tratamiento no es válido.");
            });
        }
    }
}
