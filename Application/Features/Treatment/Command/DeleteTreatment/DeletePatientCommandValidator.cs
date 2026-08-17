using FluentValidation;

namespace Application.Features.Treatment.Command.DeleteTreatment
{
    public class DeleteTreatmentCommandValidator : AbstractValidator<DeleteTreatmentCommand>
    {
        public DeleteTreatmentCommandValidator()
        {
            RuleFor(x => x.IdTreatment)
                .GreaterThan(0)
                .WithMessage("El tratamiento es obligatorio.");
        }
    }
}
