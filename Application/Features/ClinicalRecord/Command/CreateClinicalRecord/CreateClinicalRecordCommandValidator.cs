using FluentValidation;

namespace Application.Features.ClinicalRecord.Command.CreateClinicalRecord
{
    public class CreateClinicalRecordCommandValidator : AbstractValidator<CreateClinicalRecordCommand>
    {
        public CreateClinicalRecordCommandValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

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

            When(x => x.IdProcedure.HasValue, () =>
            {
                RuleFor(x => x.IdProcedure)
                    .GreaterThan(0)
                    .WithMessage("El procedimiento no es válido.");
            });

            RuleFor(x => x.RecordedAt)
                .NotEmpty()
                .WithMessage("La fecha de registro es obligatoria.")
                .Must(recordedAt => recordedAt != default)
                .WithMessage("Ingrese una fecha de registro válida.")
                .Must(recordedAt => recordedAt <= DateTime.UtcNow.AddMinutes(5))
                .WithMessage("La fecha de registro no puede ser futura.");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("La razón es obligatoria.")
                .MaximumLength(255)
                .WithMessage("La razón no puede tener más de 255 caracteres.");

            RuleFor(x => x.Diagnosis)
                .MaximumLength(1000)
                .WithMessage("La diagnóstico no puede tener más de 1000 caracteres.");

            RuleFor(x => x.Evolution)
                .MaximumLength(2000)
                .WithMessage("La evolución no puede tener más de 2000 caracteres.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000)
                .WithMessage("Las notas no pueden tener más de 1000 caracteres.");
        

           
        }
    }
}