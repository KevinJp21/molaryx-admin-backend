using FluentValidation;

namespace Application.Features.Patients.Command.DeletePatient
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");
        }
    }
}
