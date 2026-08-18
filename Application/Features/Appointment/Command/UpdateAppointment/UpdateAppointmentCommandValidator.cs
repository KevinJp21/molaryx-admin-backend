using Domain.Enums;
using FluentValidation;

namespace Application.Features.Appointment.Command.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(x => x.IdAppointment)
                .GreaterThan(0)
                .WithMessage("La cita es obligatoria.");

            When(x => x.IdPatient.HasValue, () =>
            {
                RuleFor(x => x.IdPatient)
                    .GreaterThan(0)
                    .WithMessage("El paciente no es válido.");
            });

            When(x => x.IdUser.HasValue, () =>
            {
                RuleFor(x => x.IdUser)
                    .GreaterThan(0)
                    .WithMessage("El profesional no es válido.");
            });

            When(x => x.IdService.HasValue, () =>
            {
                RuleFor(x => x.IdService)
                    .GreaterThan(0)
                    .WithMessage("El servicio no es válido.");
            });

            When(x => x.IdPatientTreatment.GetValueOrDefault() != 0, () =>
            {
                RuleFor(x => x.IdPatientTreatment)
                    .GreaterThan(0)
                    .WithMessage("El tratamiento del paciente no es válido.");
            });

            RuleFor(x => x)
                .Must(x => !(x.IdPatientTreatment.GetValueOrDefault() > 0
                    && x.Price.GetValueOrDefault() > 0))
                .WithMessage("La cita no puede tener plan de tratamiento y precio a la vez.");

            When(x => x.Price.GetValueOrDefault() != 0, () =>
            {
                RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("El precio debe ser mayor a 0.")
                    .Must(price => price == Math.Round(price!.Value, 2))
                    .WithMessage("El precio solo admite hasta 2 decimales.");
            });

            When(x => x.IdAppointmentStatus.HasValue, () =>
            {
                RuleFor(x => x.IdAppointmentStatus)
                    .Must(status => Enum.IsDefined(typeof(AppointmentStatusEnum), status!.Value))
                    .WithMessage("El estado de la cita no es válido.");
            });

            When(x => x.StartAt.HasValue || x.EndAt.HasValue, () =>
            {
                RuleFor(x => x.StartAt)
                    .NotNull()
                    .WithMessage("La fecha y hora de inicio son obligatorias al editar el horario.")
                    .GreaterThan(DateTime.Now)
                    .WithMessage("La fecha y hora de inicio debe ser mayor a la fecha y hora actual.");

                RuleFor(x => x.EndAt)
                    .NotNull()
                    .WithMessage("La fecha y hora de fin son obligatorias al editar el horario.")
                    .GreaterThan(x => x.StartAt)
                    .When(x => x.StartAt.HasValue)
                    .WithMessage("La hora de fin debe ser posterior a la de inicio.");
            });

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Las notas ingresadas son demasiado largas.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));
        }
    }
}
