using Application.Common.Regex;
using Application.Common.Validation;
using Domain.Constants;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Users.Command.CreateMember
{
    public class CreateMemberValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberValidator()
        {
            RuleFor(x => x.IdUserRole)
                .Must(role => role is
                    (short)UserRoleEnum.PROFESSIONAL or
                    (short)UserRoleEnum.ASSISTANT)
                .WithMessage("El rol de usuario solo puede ser profesional o asistente.");

            RuleFor(x => x.Username)
                .NotEmpty()
                    .WithMessage("El nombre de usuario es obligatorio.")
                    .MaximumLength(FieldLengths.Username)
                    .WithMessage("El usuario ingresado es demasiado largo.")
                    .Matches(RegexCatalog.USERNAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Username
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un nombre de usuario válido."
                    );
            RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .WithMessage("El nombre es obligatorio.")
                    .MaximumLength(FieldLengths.PersonName)
                    .WithMessage("El nombre ingresado es demasiado largo.")
                    .Matches(RegexCatalog.NAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.FirstName
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un nombre válido."
                    );

            RuleFor(x => x.SecondName)
                .MaximumLength(FieldLengths.PersonName)
                .WithMessage(
                    "El segundo nombre ingresado es demasiado largo."
                )
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(
                        x.SecondName
                    ),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage(
                    "Ingrese un nombre válido."
                );

            RuleFor(x => x.FirstSurname)
                .NotEmpty()
                .WithMessage(
                    "El primer apellido es obligatorio."
                )
                .MaximumLength(FieldLengths.PersonName)
                .WithMessage(
                    "El primer apellido ingresado es demasiado largo."
                )
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(
                        x.FirstSurname
                    ),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage(
                    "Ingrese un apellido válido."
                );

            RuleFor(x => x.SecondSurname)
                .MaximumLength(FieldLengths.PersonName)
                .WithMessage(
                    "El segundo apellido ingresado es demasiado largo."
                )
                .Matches(RegexCatalog.NAME)
                .When(
                    x => !string.IsNullOrWhiteSpace(
                        x.SecondSurname
                    ),
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage(
                    "Ingrese un apellido válido."
                );

            RuleFor(x => x.IdIdentificationType)
                    .Must(IdentificationValidation.IsAllowedForUser)
                    .WithMessage(
                        "El tipo de identificación no es válido."
                    );

            RuleFor(x => x.IdentificationNumber)
                    .NotEmpty()
                    .WithMessage(
                        "El número de identificación es obligatorio."
                    )
                    .MaximumLength(FieldLengths.IdentificationNumber)
                    .WithMessage("El número de identificación ingresado es demasiado largo.")
                    .Must((cmd, number) =>
                        IdentificationValidation.MatchesType(
                            cmd.IdIdentificationType,
                            number
                        ))
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.IdentificationNumber
                        )
                        && IdentificationValidation.IsAllowedForUser(
                            x.IdIdentificationType
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un número de identificación válido."
                    );
            RuleFor(x => x.BirthDate)
                    .NotEmpty()
                    .WithMessage(
                        "La fecha de nacimiento es obligatoria."
                    )
                    .Must(birthDate => IdentificationValidation.IsAdult(birthDate))
                    .WithMessage(
                        "El miembro debe ser mayor de edad para registrarse."
                    );

            RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .WithMessage(
                        "El número de celular es obligatorio."
                    )
                    .MaximumLength(FieldLengths.PhoneNumber)
                    .WithMessage("El número de celular ingresado es demasiado largo.")
                    .Matches(RegexCatalog.PHONE_NUMBER)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.PhoneNumber
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un número de celular válido."
                    );

            RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage(
                        "El correo electrónico es obligatorio."
                    )
                    .MaximumLength(FieldLengths.Email)
                    .WithMessage("El correo electrónico ingresado es demasiado largo.")
                    .Matches(RegexCatalog.EMAIL)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Email
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un correo electrónico válido."
                    );
        }
    }
}