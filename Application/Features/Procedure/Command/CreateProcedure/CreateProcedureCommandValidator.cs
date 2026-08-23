using Application.Common.Regex;
using FluentValidation;

namespace Application.Features.Procedure.Command.CreateProcedure
{
    public class CreateProcedureCommandValidator : AbstractValidator<CreateProcedureCommand>
    {
        public CreateProcedureCommandValidator()
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

            When(x => x.ReferencePrice.HasValue, () =>
            {
                RuleFor(x => x.ReferencePrice)
                    .GreaterThan(0)
                    .WithMessage("El precio de referencia debe ser mayor a 0.")
                    .Must(price => price == Math.Round(price!.Value, 2))
                    .WithMessage("El precio de referencia solo admite hasta 2 decimales.");
            });
        }
    }
}
