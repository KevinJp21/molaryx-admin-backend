using Domain.Constants;
using FluentValidation;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .MaximumLength(FieldLengths.Email)
                .WithMessage("El correo electrónico ingresado es demasiado largo.")
                .EmailAddress()
                .WithMessage("El correo electrónico no es válido");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("La contraseña es obligatoria.");
        }
    }
}