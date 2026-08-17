using FluentValidation;

namespace Application.Features.Treatment.Command.CreateTreatment
{
    public class CreateTreatmentCommandValidator : AbstractValidator<CreateTreatmentCommand>
    {
        public CreateTreatmentCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(255)
                .WithMessage("El nombre ingresado es demasiado largo.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("La descripción ingresada es demasiado larga.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
