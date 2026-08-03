using Application.Common.Regex;
using Application.Features.Auth.Command.ResetPassword;
using FluentValidation;

namespace Application.Features.Auth.Command.ForgotPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(r => r.Token)
                .NotEmpty()
                .WithMessage("El token es obligatorio.");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("La contraseña es obligatoria.")
                .Matches(RegexCatalog.PASSWORD)
                    .WithMessage(
                        "La contraseña debe tener mínimo 8 caracteres, " +
                        "una mayúscula, una minúscula, un número y un carácter especial."
                    );

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty()
                .WithMessage("La confirmación de la contraseña es obligatoria.")
                .Equal(r => r.Password)
                .WithMessage("Las contraseñas no coinciden.");


        }
    }
}