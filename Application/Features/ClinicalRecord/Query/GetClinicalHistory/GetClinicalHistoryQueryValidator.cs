using FluentValidation;

namespace Application.Features.ClinicalRecord.Query.GetClinicalHistory
{
    public class GetClinicalHistoryQueryValidator : AbstractValidator<GetClinicalHistoryQuery>
    {
        public GetClinicalHistoryQueryValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

            When(x => x.From.HasValue && x.To.HasValue, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.From <= x.To)
                    .WithMessage("La fecha inicial no puede ser posterior a la fecha final.");
            });
        }
    }
}
