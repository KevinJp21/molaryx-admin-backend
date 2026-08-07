using Application.Common.Regex;
using Application.Common.Validation;
using FluentValidation;

namespace Application.Features.Tenant.Command.CreateBusinessTenant
{
    public class CreateBusinessTenantCommandValidator
        : AbstractValidator<CreateBusinessTenantCommand>
    {
        public CreateBusinessTenantCommandValidator()
        {
            RuleFor(x => x.Tenant)
                .NotNull()
                .WithMessage("Los datos del consultorio son obligatorios.");

            RuleFor(x => x.Owner)
                .NotNull()
                .WithMessage("Los datos del propietario son obligatorios.");
            
            When(x => x.Tenant is not null, () =>
            {
                RuleFor(x => x.Tenant.ConsultoryName)
                    .NotEmpty()
                    .WithMessage("El nombre del consultorio es obligatorio.")
                    .MaximumLength(150)
                    .WithMessage("El nombre ingresado es demasiado largo.");

                RuleFor(x => x.Tenant.Email)
                    .NotEmpty()
                    .WithMessage("El correo electrónico es obligatorio.")
                    .Matches(RegexCatalog.EMAIL)
                    .When(
                        x => !string.IsNullOrWhiteSpace(x.Tenant.Email),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("El correo electrónico no es válido.");

                RuleFor(x => x.Tenant.PhoneNumber)
                    .NotEmpty()
                    .WithMessage("El número de celular es obligatorio.")
                    .Matches(RegexCatalog.PHONE_NUMBER)
                    .When(
                        x => !string.IsNullOrWhiteSpace(x.Tenant.PhoneNumber),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage("El número de celular no es válido.");

                RuleFor(x => x.Tenant.Address)
                    .NotEmpty()
                    .WithMessage("La dirección es obligatoria.")
                    .MaximumLength(255)
                    .WithMessage("La dirección ingresada es demasiado larga.");

                When(
                    x => x.Tenant.IdIdentificationType.HasValue,
                    () =>
                    {
                        RuleFor(x => x.Tenant.IdIdentificationType!.Value)
                            .Must(IdentificationValidation.IsValidType)
                            .WithMessage(
                                "El tipo de identificación del consultorio no es válido."
                            );

                        RuleFor(x => x.Tenant.IdentificationNumber)
                            .NotEmpty()
                            .WithMessage(
                                "El número de identificación es obligatorio."
                            )
                            .Must((cmd, number) =>
                                IdentificationValidation.MatchesType(
                                    cmd.Tenant.IdIdentificationType!.Value,
                                    number
                                ))
                            .When(
                                x => !string.IsNullOrWhiteSpace(
                                    x.Tenant.IdentificationNumber
                                ),
                                ApplyConditionTo.CurrentValidator
                            )
                            .WithMessage(
                                "El número de identificación no es válido."
                            );
                    }
                );
            });

            When(x => x.Owner is not null, () =>
            {
                RuleFor(x => x.Owner.Username)
                    .NotEmpty()
                    .WithMessage("El nombre de usuario es obligatorio.")
                    .MaximumLength(30)
                    .WithMessage("El usuario ingresado es demasiado largo.")
                    .Matches(RegexCatalog.USERNAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.Username
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un nombre de usuario válido."
                    );

                RuleFor(x => x.Owner.FirstName)
                    .NotEmpty()
                    .WithMessage("El nombre es obligatorio.")
                    .MaximumLength(100)
                    .WithMessage("El nombre ingresado es demasiado largo.")
                    .Matches(RegexCatalog.NAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.FirstName
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un nombre válido."
                    );

                RuleFor(x => x.Owner.SecondName)
                    .MaximumLength(100)
                    .WithMessage(
                        "El segundo nombre ingresado es demasiado largo."
                    )
                    .Matches(RegexCatalog.NAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.SecondName
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un nombre válido."
                    );

                RuleFor(x => x.Owner.FirstSurname)
                    .NotEmpty()
                    .WithMessage(
                        "El primer apellido es obligatorio."
                    )
                    .MaximumLength(100)
                    .WithMessage(
                        "El primer apellido ingresado es demasiado largo."
                    )
                    .Matches(RegexCatalog.NAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.FirstSurname
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un apellido válido."
                    );

                RuleFor(x => x.Owner.SecondSurname)
                    .MaximumLength(100)
                    .WithMessage(
                        "El segundo apellido ingresado es demasiado largo."
                    )
                    .Matches(RegexCatalog.NAME)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.SecondSurname
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un apellido válido."
                    );

                RuleFor(x => x.Owner.IdIdentificationType)
                    .Must(IdentificationValidation.IsAllowedForOwner)
                    .WithMessage(
                        "El tipo de identificación del propietario no es válido. Use CC o CE."
                    );

                RuleFor(x => x.Owner.IdentificationNumber)
                    .NotEmpty()
                    .WithMessage(
                        "El número de identificación es obligatorio."
                    )
                    .Must((cmd, number) =>
                        IdentificationValidation.MatchesType(
                            cmd.Owner.IdIdentificationType,
                            number
                        ))
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.IdentificationNumber
                        )
                        && IdentificationValidation.IsAllowedForOwner(
                            x.Owner.IdIdentificationType
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un número de identificación válido."
                    );

                RuleFor(x => x.Owner.BirthDate)
                    .NotEmpty()
                    .WithMessage(
                        "La fecha de nacimiento es obligatoria."
                    )
                    .Must(birthDate => IdentificationValidation.IsAdult(birthDate))
                    .WithMessage(
                        "El propietario debe ser mayor de 18 años."
                    );

                RuleFor(x => x.Owner.PhoneNumber)
                    .NotEmpty()
                    .WithMessage(
                        "El número de celular es obligatorio."
                    )
                    .Matches(RegexCatalog.PHONE_NUMBER)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.PhoneNumber
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un número de celular válido."
                    );

                RuleFor(x => x.Owner.Email)
                    .NotEmpty()
                    .WithMessage(
                        "El correo electrónico es obligatorio."
                    )
                    .Matches(RegexCatalog.EMAIL)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.Email
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "Ingrese un correo electrónico válido."
                    );

                RuleFor(x => x.Owner.Password)
                    .NotEmpty()
                    .WithMessage(
                        "La contraseña es obligatoria."
                    )
                    .Matches(RegexCatalog.PASSWORD)
                    .When(
                        x => !string.IsNullOrWhiteSpace(
                            x.Owner.Password
                        ),
                        ApplyConditionTo.CurrentValidator
                    )
                    .WithMessage(
                        "La contraseña debe tener mínimo 8 caracteres, " +
                        "una mayúscula, una minúscula, un número y un carácter especial."
                    );
            });
        }
    }
}