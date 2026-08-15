using Application.Common.Regex;
using FluentValidation;

namespace Application.Features.Service.Command.UpdateService
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            When(x => x.Name is not null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("El nombre es obligatorio.")
                    .MaximumLength(100)
                    .WithMessage("El nombre ingresado es demasiado largo.");
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
