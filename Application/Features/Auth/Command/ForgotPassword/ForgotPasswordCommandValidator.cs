using Application.Features.Auth.Command.ForgotPassword;
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
                .EmailAddress()
                .WithMessage("El correo electrónico no es valido");
        }
    }
}