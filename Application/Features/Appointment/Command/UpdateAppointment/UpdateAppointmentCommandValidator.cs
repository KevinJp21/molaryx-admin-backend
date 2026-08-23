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

            When(x => x.IdPatientTreatment.GetValueOrDefault() != 0, () =>
            {
                RuleFor(x => x.IdPatientTreatment)
                    .GreaterThan(0)
                    .WithMessage("El tratamiento del paciente no es válido.");
            });

            When(x => x.Procedures is not null, () =>
            {
                RuleFor(x => x.Procedures!)
                    .NotEmpty()
                    .WithMessage("Debe incluir al menos un procedimiento.");

                RuleFor(x => x.Procedures!)
                    .Must(procedures => procedures
                        .Select(p => p.IdProcedure)
                        .Distinct()
                        .Count() == procedures.Count)
                    .When(x => x.Procedures is { Count: > 0 })
                    .WithMessage("No se puede repetir el mismo procedimiento en la cita.");

                RuleForEach(x => x.Procedures).ChildRules(procedure =>
                {
                    procedure.RuleFor(p => p.IdProcedure)
                        .GreaterThan(0)
                        .WithMessage("El procedimiento no es válido.");

                    procedure.RuleFor(p => p.Price)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("El precio no puede ser negativo.")
                        .Must(price => price == Math.Round(price, 2))
                        .WithMessage("El precio solo admite hasta 2 decimales.");

                    procedure.RuleFor(p => p.Notes)
                        .MaximumLength(500)
                        .WithMessage("Las notas del procedimiento son demasiado largas.")
                        .When(p => !string.IsNullOrWhiteSpace(p.Notes));
                });
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
