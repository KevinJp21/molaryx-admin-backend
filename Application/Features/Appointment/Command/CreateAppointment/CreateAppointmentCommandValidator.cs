using Domain.Enums;
using FluentValidation;

namespace Application.Features.Appointment.Command.CreateAppointment
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

            RuleFor(x => x.IdUser)
                .GreaterThan(0)
                .WithMessage("El profesional es obligatorio.");

            RuleFor(x => x.IdService)
                .GreaterThan(0)
                .WithMessage("El servicio es obligatorio.");

            RuleFor(x => x.StartAt)
                .NotEmpty()
                .WithMessage("La fecha y hora de inicio son obligatorias.")
                .Must(startAt => startAt != default)
                .WithMessage("Ingrese una fecha y hora de inicio válida.");

            RuleFor(x => x.EndAt)
                .NotEmpty()
                .WithMessage("La fecha y hora de fin son obligatorias.")
                .GreaterThan(x => x.StartAt)
                .When(x => x.StartAt != default)
                .WithMessage("La hora de fin debe ser posterior a la de inicio.");

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Las notas ingresadas son demasiado largas.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));
        }
    }
}
