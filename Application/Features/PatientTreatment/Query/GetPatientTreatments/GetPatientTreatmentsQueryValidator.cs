using Domain.Enums;
using FluentValidation;

namespace Application.Features.PatientTreatment.Query.GetPatientTreatments
{
    public class GetPatientTreatmentsQueryValidator : AbstractValidator<GetPatientTreatmentsQuery>
    {
        public GetPatientTreatmentsQueryValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

            When(x => x.IdTreatmentStatus.HasValue, () =>
            {
                RuleFor(x => x.IdTreatmentStatus)
                    .Must(status => Enum.IsDefined((TreatmentStatusEnum)status!.Value))
                    .WithMessage("El estado del tratamiento no es válido.");
            });
        }
    }
}
