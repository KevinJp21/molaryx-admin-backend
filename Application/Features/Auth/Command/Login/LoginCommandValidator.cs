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
                .EmailAddress()
                .WithMessage("El correo electrónico no es valido");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("La contraseña es obligatoria.");
        }
    }
}