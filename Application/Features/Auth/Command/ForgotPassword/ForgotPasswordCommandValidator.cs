using Domain.Constants;
using FluentValidation;

namespace Application.Features.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(f => f.Email)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .MaximumLength(FieldLengths.Email)
                .WithMessage("El correo electrónico ingresado es demasiado largo.")
                .EmailAddress()
                .WithMessage("El correo electrónico no es válido");
        }
    }
}