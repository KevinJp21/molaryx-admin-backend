using Application.Common.Regex;
using Application.Common.Validation;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Patients.Command.UpdatePatient
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(x => x.IdPatient)
                .GreaterThan(0)
                .WithMessage("El paciente es obligatorio.");

            When(x => x.FirstName is not null, () =>
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
            });

            When(x => x.SecondName is not null, () =>
            {
                RuleFor(x => x.SecondName)
                    .MaximumLength(100)
                    .WithMessage("El segundo nombre ingresado es demasiado largo.")
                    .Matches(RegexCatalog.NAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(x.SecondName),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("Ingrese un nombre válido.");
            });

            When(x => x.FirstSurname is not null, () =>
            {
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
            });

            When(x => x.SecondSurname is not null, () =>
            {
                RuleFor(x => x.SecondSurname)
                    .MaximumLength(100)
                    .WithMessage("El segundo apellido ingresado es demasiado largo.")
                    .Matches(RegexCatalog.NAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(x.SecondSurname),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("Ingrese un apellido válido.");
            });

            When(x => x.IdIdentificationType.HasValue, () =>
            {
                RuleFor(x => x.IdIdentificationType)
                    .Must(type => IdentificationValidation.IsAllowedForPatient(type!.Value))
                    .WithMessage("El tipo de identificación no es válido.");
            });

            When(x => x.IdentificationNumber is not null, () =>
            {
                RuleFor(x => x.IdentificationNumber)
                    .NotEmpty()
                    .WithMessage("El número de identificación es obligatorio.")
                    .Must((cmd, number) =>
                        IdentificationValidation.MatchesType(
                            cmd.IdIdentificationType!.Value,
                            number
                        ))
                    .When(
                        x => !string.IsNullOrWhiteSpace(x.IdentificationNumber)
                            && x.IdIdentificationType.HasValue
                            && IdentificationValidation.IsAllowedForPatient(x.IdIdentificationType.Value),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("Ingrese un número de identificación válido.");
            });

            When(x => x.BirthDate.HasValue, () =>
            {
                RuleFor(x => x.BirthDate)
                    .Must(birthDate => birthDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                    .WithMessage("La fecha de nacimiento no es válida.")
                    .Must(birthDate => IdentificationValidation.IsAdult(birthDate!.Value))
                    .When(
                        x => x.IdIdentificationType is
                            (short)IdentificationTypeEnum.CC or
                            (short)IdentificationTypeEnum.CE,
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("El paciente debe ser mayor de edad con la identificación seleccionada.");
            });

            When(x => x.PhoneNumber is not null, () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .WithMessage("El número de celular es obligatorio.")
                    .Matches(RegexCatalog.PHONE_NUMBER)
                    .When(
                        x => !string.IsNullOrWhiteSpace(x.PhoneNumber),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("Ingrese un número de celular válido.");
            });

            When(x => x.Email is not null, () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("El correo electrónico es obligatorio.")
                    .Matches(RegexCatalog.EMAIL)
                    .When(
                        x => !string.IsNullOrWhiteSpace(x.Email),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("Ingrese un correo electrónico válido.");
            });
        }
    }
}
