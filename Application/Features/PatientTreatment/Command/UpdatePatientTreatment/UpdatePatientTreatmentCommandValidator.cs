using Domain.Enums;
using FluentValidation;

namespace Application.Features.PatientTreatment.Command.UpdatePatientTreatment
{
    public class UpdatePatientTreatmentCommandValidator : AbstractValidator<UpdatePatientTreatmentCommand>
    {
        public UpdatePatientTreatmentCommandValidator()
        {
            RuleFor(x => x.IdPatientTreatment)
                .GreaterThan(0)
                .WithMessage("El tratamiento del paciente es obligatorio.");

            When(x => x.AgreedPrice.HasValue, () =>
            {
                RuleFor(x => x.AgreedPrice)
                    .GreaterThan(0)
                    .WithMessage("El precio acordado debe ser mayor a 0.");
            });

            When(x => x.IdPaymentFrequency.HasValue, () =>
            {
                RuleFor(x => x.IdPaymentFrequency)
                    .Must(frequency => Enum.IsDefined((PaymentFrequencyEnum)frequency!.Value))
                    .WithMessage("La frecuencia de pago no es válida.");
            });

            When(x => x.PeriodicAmount.HasValue, () =>
            {
                RuleFor(x => x.PeriodicAmount)
                    .GreaterThan(0)
                    .WithMessage("El monto periódico debe ser mayor a 0.");
            });

            When(x => x.StartAt.HasValue, () =>
            {
                RuleFor(x => x.StartAt)
                    .Must(startAt => startAt != default)
                    .WithMessage("Ingrese una fecha de inicio válida.");
            });

            When(x => x.IdPatientTreatmentStatus.HasValue, () =>
            {
                RuleFor(x => x.IdPatientTreatmentStatus)
                    .Must(status => Enum.IsDefined((PatientTreatmentStatusEnum)status!.Value))
                    .WithMessage("El estado del tratamiento no es válido.");
            });

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Las notas ingresadas son demasiado largas.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));
        }
    }
}
