using Application.Common.Regex;
using FluentValidation;

namespace Application.Features.Service.Command.CreateService
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre ingresado es demasiado largo.")
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(x.Name),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un nombre válido.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("La descripción ingresada es demasiado larga.")
                .When(
                    x => !string.IsNullOrWhiteSpace(x.Description),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese una descripción válida.");
        }
    }
}
