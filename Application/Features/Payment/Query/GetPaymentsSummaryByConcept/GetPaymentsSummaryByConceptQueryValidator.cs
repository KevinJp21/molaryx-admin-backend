using FluentValidation;

namespace Application.Features.Payment.Query.GetPaymentsSummaryByConcept
{
    public class GetPaymentsSummaryByConceptQueryValidator
        : AbstractValidator<GetPaymentsSummaryByConceptQuery>
    {
        public GetPaymentsSummaryByConceptQueryValidator()
        {
            RuleFor(x => x)
                .Must(x => x.IdAppointment.GetValueOrDefault() > 0
                    ^ x.IdPatientTreatment.GetValueOrDefault() > 0)
                .WithMessage("Indique exactamente un concepto: cita o tratamiento del paciente.");

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
