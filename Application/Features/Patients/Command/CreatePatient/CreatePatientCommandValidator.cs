using Application.Common.Regex;
using Application.Common.Validation;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Patients.Command.CreatePatient
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre ingresado es demasiado largo.")
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(x.FirstName),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un nombre válido.");

            RuleFor(x => x.SecondName)
                .MaximumLength(100)
                .WithMessage("El segundo nombre ingresado es demasiado largo.")
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(x.SecondName),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un nombre válido.");

            RuleFor(x => x.FirstSurname)
                .NotEmpty()
                .WithMessage("El primer apellido es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El primer apellido ingresado es demasiado largo.")
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(x.FirstSurname),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un apellido válido.");

            RuleFor(x => x.SecondSurname)
                .MaximumLength(100)
                .WithMessage("El segundo apellido ingresado es demasiado largo.")
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(x.SecondSurname),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un apellido válido.");

            RuleFor(x => x.IdIdentificationType)
                .Must(IdentificationValidation.IsAllowedForPatient)
                .WithMessage("El tipo de identificación no es válido.");

            RuleFor(x => x.IdentificationNumber)
                .NotEmpty()
                .WithMessage("El número de identificación es obligatorio.")
                .Must((cmd, number) =>
                    IdentificationValidation.MatchesType(
                        cmd.IdIdentificationType,
                        number
                    ))
                .When(
                    x => !string.IsNullOrWhiteSpace(x.IdentificationNumber)
                        && IdentificationValidation.IsAllowedForPatient(x.IdIdentificationType),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un número de identificación válido.");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("La fecha de nacimiento es obligatoria.")
                .Must(birthDate => birthDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("La fecha de nacimiento no es válida.")
                .Must(birthDate => IdentificationValidation.IsAdult(birthDate))
                .When(
                    x => x.IdIdentificationType is
                        (short)IdentificationTypeEnum.CC or
                        (short)IdentificationTypeEnum.CE,
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("El paciente debe ser mayor de edad con la identificación seleccionada.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("El número de celular es obligatorio.")
                .Matches(RegexCatalog.PHONE_NUMBER)
                .When(
                    x => !string.IsNullOrWhiteSpace(x.PhoneNumber),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un número de celular válido.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .Matches(RegexCatalog.EMAIL)
                .When(
                    x => !string.IsNullOrWhiteSpace(x.Email),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("Ingrese un correo electrónico válido.");
        }
    }
}
