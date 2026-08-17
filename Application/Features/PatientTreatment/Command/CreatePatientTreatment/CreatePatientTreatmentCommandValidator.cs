using Domain.Common;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.PatientTreatment.Command.CreatePatientTreatment
{
    public class CreatePatientTreatmentCommandValidator : AbstractValidator<CreatePatientTreatmentCommand>
    {
        public CreatePatientTreatmentCommandValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

            RuleFor(x => x.IdTreatment)
                .GreaterThan(0)
                .WithMessage("El tratamiento es obligatorio.");

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

                RuleFor(x => x.IdPaymentFrequency)
                    .NotNull()
                    .WithMessage("La frecuencia de pago es obligatoria cuando hay un monto periódico.");
            });

            When(
                x => x.IdPaymentFrequency.HasValue
                    && x.IdPaymentFrequency.Value != (short)PaymentFrequencyEnum.ONE_TIME,
                () =>
                {
                    RuleFor(x => x.PeriodicAmount)
                        .NotNull()
                        .WithMessage("El monto periódico es obligatorio para esta frecuencia de pago.")
                        .GreaterThan(0)
                        .WithMessage("El monto periódico debe ser mayor a 0.");
                });

            RuleFor(x => x.StartAt)
                .NotEmpty()
                .WithMessage("La fecha de inicio es obligatoria.")
                .Must(startAt => startAt != default)
                .WithMessage("Ingrese una fecha de inicio válida.");

            When(x => x.EndAt.HasValue, () =>
            {
                RuleFor(x => x.EndAt)
                    .GreaterThan(x => x.StartAt)
                    .When(x => x.StartAt != default)
                    .WithMessage("La fecha de fin debe ser posterior a la de inicio.");
            });

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Las notas ingresadas son demasiado largas.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));
        }
    }
}
