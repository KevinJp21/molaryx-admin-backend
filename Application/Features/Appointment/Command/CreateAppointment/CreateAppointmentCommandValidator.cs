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

            When(x => x.IdPatientTreatment.GetValueOrDefault() != 0, () =>
            {
                RuleFor(x => x.IdPatientTreatment)
                    .GreaterThan(0)
                    .WithMessage("El tratamiento del paciente no es válido.");
            });

            RuleFor(x => x.Procedures)
                .NotEmpty()
                .WithMessage("Debe incluir al menos un procedimiento.");

            RuleFor(x => x.Procedures)
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

            RuleFor(x => x)
                .Must(x => x.Procedures.All(p => p.Price == 0))
                .When(x => x.IdPatientTreatment.GetValueOrDefault() > 0 && x.Procedures is { Count: > 0 })
                .WithMessage("Los procedimientos de una cita con plan de tratamiento deben tener precio 0.");

            RuleFor(x => x)
                .Must(x => x.Procedures.Sum(p => p.Price) > 0)
                .When(x => x.IdPatientTreatment.GetValueOrDefault() <= 0 && x.Procedures is { Count: > 0 })
                .WithMessage("La cita debe tener al menos un procedimiento con precio mayor a 0.");

            RuleFor(x => x.StartAt)
                .NotEmpty()
                .WithMessage("La fecha y hora de inicio son obligatorias.")
                .GreaterThan(DateTime.Now)
                .WithMessage("La fecha y hora de inicio debe ser mayor a la fecha y hora actual.")
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
