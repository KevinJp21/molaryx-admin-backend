using FluentValidation;

namespace Application.Features.Payment.Query.GetPaymentReport
{
    public class GetPaymentReportQueryValidator : AbstractValidator<GetPaymentReportQuery>
    {
        public GetPaymentReportQueryValidator()
        {
            When(x => x.From.HasValue, () =>
            {
                RuleFor(x => x.From)
                    .Must(from => from != default)
                    .WithMessage("Ingrese una fecha inicial válida.");
            });

            When(x => x.To.HasValue, () =>
            {
                RuleFor(x => x.To)
                    .Must(to => to != default)
                    .WithMessage("Ingrese una fecha final válida.");
            });

            RuleFor(x => x)
                .Must(x => x.From.HasValue == x.To.HasValue)
                .WithMessage("Indique ambas fechas del periodo o ninguna.");

            RuleFor(x => x)
                .Must(x => x.From!.Value < x.To!.Value)
                .WithMessage("La fecha inicial debe ser anterior a la fecha final.")
                .When(x => x.From.HasValue && x.To.HasValue
                    && x.From != default && x.To != default);

            RuleFor(x => x)
                .Must(x =>
                {
                    var filters = 0;
                    if (x.IdPatient.GetValueOrDefault() > 0) filters++;
                    if (x.IdAppointment.GetValueOrDefault() > 0) filters++;
                    if (x.IdPatientTreatment.GetValueOrDefault() > 0) filters++;
                    return filters <= 1;
                })
                .WithMessage("Indique como máximo un filtro: paciente, cita o tratamiento del paciente.");

            When(x => x.IdPatient.HasValue, () =>
            {
                RuleFor(x => x.IdPatient)
                    .GreaterThan(0)
                    .WithMessage("El paciente no es válido.");
            });

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
