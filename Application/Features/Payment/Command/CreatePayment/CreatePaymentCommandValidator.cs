using Domain.Enums;
using FluentValidation;

namespace Application.Features.Payment.Command.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

            RuleFor(x => x)
                .Must(x => x.IdAppointment.GetValueOrDefault() > 0
                    ^ x.IdPatientTreatment.GetValueOrDefault() > 0)
                .WithMessage("El pago debe asociarse exactamente a una cita o a un tratamiento del paciente.");

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

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("El monto debe ser mayor a 0.");

            RuleFor(x => x.PaidAt)
                .NotEmpty()
                .WithMessage("La fecha de pago es obligatoria.")
                .Must(paidAt => paidAt != default)
                .WithMessage("Ingrese una fecha de pago válida.");

            RuleFor(x => x.IdPaymentMethod)
                .Must(method => Enum.IsDefined((PaymentMethodEnum)method))
                .WithMessage("El método de pago no es válido.");

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Las notas ingresadas son demasiado largas.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));
        }
    }
}
