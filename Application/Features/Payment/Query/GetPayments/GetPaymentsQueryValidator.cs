using FluentValidation;

namespace Application.Features.Payment.Query.GetPayments
{
    public class GetPaymentsQueryValidator : AbstractValidator<GetPaymentsQuery>
    {
        public GetPaymentsQueryValidator()
        {
            RuleFor(x => x)
                .Must(x =>
                {
                    var filters = 0;
                    if (x.IdPatient.GetValueOrDefault() > 0) filters++;
                    if (x.IdAppointment.GetValueOrDefault() > 0) filters++;
                    if (x.IdPatientTreatment.GetValueOrDefault() > 0) filters++;
                    return filters == 1;
                })
                .WithMessage("Indique solo un filtro: paciente, cita o tratamiento del paciente.");

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
