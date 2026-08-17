using FluentValidation;

namespace Application.Features.Treatment.Command.UpdateTreatment
{
    public class UpdateTreatmentCommandValidator : AbstractValidator<UpdateTreatmentCommand>
    {
        public UpdateTreatmentCommandValidator()
        {
            RuleFor(x => x.IdTreatment)
                .GreaterThan(0)
                .WithMessage("El tratamiento es obligatorio.");

            When(x => x.Name is not null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("El nombre es obligatorio.");
            });

            When(x => x.Description is not null, () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(500)
                        .WithMessage("La descripción ingresada es demasiado larga.")
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Description),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese una descripción válida.");
            });
        }
    }
}
