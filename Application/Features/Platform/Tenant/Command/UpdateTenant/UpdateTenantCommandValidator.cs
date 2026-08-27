using Application.Common.Regex;
using Application.Common.Validation;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Platform.Tenant.Command.UpdateTenant
{
    public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
    {
        public UpdateTenantCommandValidator()
        {
            RuleFor(x => x.IdTenant)
                .GreaterThan(0)
                .WithMessage("El consultorio es obligatorio.");

            RuleFor(x => x)
                .Must(x => x.Tenant is not null || x.Owner is not null || x.Subscription is not null)
                .WithMessage("Debe enviar al menos los datos del consultorio, del propietario o de la suscripción.");

            When(x => x.Tenant is not null, () =>
            {
                When(x => x.Tenant!.ConsultoryName is not null, () =>
                {
                    RuleFor(x => x.Tenant!.ConsultoryName)
                        .NotEmpty()
                        .WithMessage("El nombre del consultorio es obligatorio.")
                        .MaximumLength(150)
                        .WithMessage("El nombre ingresado es demasiado largo.");
                });

                When(x => x.Tenant!.Email is not null, () =>
                {
                    RuleFor(x => x.Tenant!.Email)
                        .NotEmpty()
                        .WithMessage("El correo electrónico es obligatorio.")
                        .Matches(RegexCatalog.EMAIL)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Tenant!.Email),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("El correo electrónico no es válido.");
                });

                When(x => x.Tenant!.PhoneNumber is not null, () =>
                {
                    RuleFor(x => x.Tenant!.PhoneNumber)
                        .NotEmpty()
                        .WithMessage("El número de celular es obligatorio.")
                        .Matches(RegexCatalog.PHONE_NUMBER)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Tenant!.PhoneNumber),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("El número de celular no es válido.");
                });

                When(x => x.Tenant!.Address is not null, () =>
                {
                    RuleFor(x => x.Tenant!.Address)
                        .NotEmpty()
                        .WithMessage("La dirección es obligatoria.")
                        .MaximumLength(255)
                        .WithMessage("La dirección ingresada es demasiado larga.");
                });

                When(x => x.Tenant!.IdIdentificationType.HasValue, () =>
                {
                    RuleFor(x => x.Tenant!.IdIdentificationType!.Value)
                        .Must(IdentificationValidation.IsValidType)
                        .WithMessage("El tipo de identificación del consultorio no es válido.");
                });

                When(x => x.Tenant!.IdentificationNumber is not null, () =>
                {
                    RuleFor(x => x.Tenant!.IdentificationNumber)
                        .NotEmpty()
                        .WithMessage("El número de identificación es obligatorio.")
                        .Must((cmd, number) =>
                            IdentificationValidation.MatchesType(
                                cmd.Tenant!.IdIdentificationType!.Value,
                                number
                            ))
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Tenant!.IdentificationNumber)
                                && x.Tenant.IdIdentificationType.HasValue
                                && IdentificationValidation.IsValidType(
                                    x.Tenant.IdIdentificationType.Value),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("El número de identificación no es válido.");
                });

                When(x => x.Tenant!.IdTenantStatus.HasValue, () =>
                {
                    RuleFor(x => x.Tenant!.IdTenantStatus)
                        .Must(status => Enum.IsDefined((TenantStatusEnum)status!.Value))
                        .WithMessage("El estado del consultorio no es válido.");
                });
            });

            When(x => x.Owner is not null, () =>
            {
                RuleFor(x => x.Owner!.IdUser)
                    .GreaterThan(0)
                    .WithMessage("El propietario es obligatorio.");

                When(x => x.Owner!.Username is not null, () =>
                {
                    RuleFor(x => x.Owner!.Username)
                        .NotEmpty()
                        .WithMessage("El nombre de usuario es obligatorio.")
                        .MaximumLength(30)
                        .WithMessage("El usuario ingresado es demasiado largo.")
                        .Matches(RegexCatalog.USERNAME)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.Username),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un nombre de usuario válido.");
                });

                When(x => x.Owner!.FirstName is not null, () =>
                {
                    RuleFor(x => x.Owner!.FirstName)
                        .NotEmpty()
                        .WithMessage("El nombre es obligatorio.")
                        .MaximumLength(100)
                        .WithMessage("El nombre ingresado es demasiado largo.")
                        .Matches(RegexCatalog.NAME)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.FirstName),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un nombre válido.");
                });

                When(x => x.Owner!.SecondName is not null, () =>
                {
                    RuleFor(x => x.Owner!.SecondName)
                        .MaximumLength(100)
                        .WithMessage("El segundo nombre ingresado es demasiado largo.")
                        .Matches(RegexCatalog.NAME)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.SecondName),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un nombre válido.");
                });

                When(x => x.Owner!.FirstSurname is not null, () =>
                {
                    RuleFor(x => x.Owner!.FirstSurname)
                        .NotEmpty()
                        .WithMessage("El primer apellido es obligatorio.")
                        .MaximumLength(100)
                        .WithMessage("El primer apellido ingresado es demasiado largo.")
                        .Matches(RegexCatalog.NAME)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.FirstSurname),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un apellido válido.");
                });

                When(x => x.Owner!.SecondSurname is not null, () =>
                {
                    RuleFor(x => x.Owner!.SecondSurname)
                        .MaximumLength(100)
                        .WithMessage("El segundo apellido ingresado es demasiado largo.")
                        .Matches(RegexCatalog.NAME)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.SecondSurname),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un apellido válido.");
                });

                When(x => x.Owner!.IdIdentificationType.HasValue, () =>
                {
                    RuleFor(x => x.Owner!.IdIdentificationType)
                        .Must(type => IdentificationValidation.IsAllowedForUser(type!.Value))
                        .WithMessage(
                            "El tipo de identificación del propietario no es válido. Use CC o CE.");
                });

                When(x => x.Owner!.IdentificationNumber is not null, () =>
                {
                    RuleFor(x => x.Owner!.IdentificationNumber)
                        .NotEmpty()
                        .WithMessage("El número de identificación es obligatorio.")
                        .Must((cmd, number) =>
                            IdentificationValidation.MatchesType(
                                cmd.Owner!.IdIdentificationType!.Value,
                                number
                            ))
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.IdentificationNumber)
                                && x.Owner.IdIdentificationType.HasValue
                                && IdentificationValidation.IsAllowedForUser(
                                    x.Owner.IdIdentificationType.Value),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un número de identificación válido.");
                });

                When(x => x.Owner!.PhoneNumber is not null, () =>
                {
                    RuleFor(x => x.Owner!.PhoneNumber)
                        .NotEmpty()
                        .WithMessage("El número de celular es obligatorio.")
                        .Matches(RegexCatalog.PHONE_NUMBER)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.PhoneNumber),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un número de celular válido.");
                });

                When(x => x.Owner!.Email is not null, () =>
                {
                    RuleFor(x => x.Owner!.Email)
                        .NotEmpty()
                        .WithMessage("El correo electrónico es obligatorio.")
                        .Matches(RegexCatalog.EMAIL)
                        .When(
                            x => !string.IsNullOrWhiteSpace(x.Owner!.Email),
                            ApplyConditionTo.CurrentValidator
                        )
                        .WithMessage("Ingrese un correo electrónico válido.");
                });

                When(x => x.Owner!.IdUserStatus.HasValue, () =>
                {
                    RuleFor(x => x.Owner!.IdUserStatus)
                        .Must(status => Enum.IsDefined((UserStatusEnum)status!.Value))
                        .WithMessage("El estado del usuario no es válido.");
                });
            });

            When(x => x.Subscription is not null, () =>
            {
                RuleFor(x => x.Subscription!.IdTenantSubscription)
                    .GreaterThan(0)
                    .WithMessage("La suscripción es obligatoria.");

                When(x => x.Subscription!.Price.HasValue, () =>
                {
                    RuleFor(x => x.Subscription!.Price)
                        .GreaterThan(0)
                        .WithMessage("El precio de la suscripción debe ser mayor que cero.");
                });

                When(x => x.Subscription!.MaxProfessionals.HasValue, () =>
                {
                    RuleFor(x => x.Subscription!.MaxProfessionals)
                        .GreaterThan((short)0)
                        .WithMessage("El máximo de profesionales debe ser mayor que cero.");
                });

                When(x => x.Subscription!.MaxAssistants.HasValue, () =>
                {
                    RuleFor(x => x.Subscription!.MaxAssistants)
                        .GreaterThan((short)0)
                        .WithMessage("El máximo de asistentes debe ser mayor que cero.");
                });

                When(x => x.Subscription!.MaxPatients.HasValue, () =>
                {
                    RuleFor(x => x.Subscription!.MaxPatients)
                        .GreaterThan(0)
                        .WithMessage("El máximo de pacientes debe ser mayor que cero.");
                });

                When(x => x.Subscription!.IdTenantSubscriptionStatus.HasValue, () =>
                {
                    RuleFor(x => x.Subscription!.IdTenantSubscriptionStatus)
                        .Must(status =>
                            Enum.IsDefined((TenantSubscriptionStatusEnum)status!.Value))
                        .WithMessage("El estado de la suscripción no es válido.");
                });

                When(
                    x => x.Subscription!.StartsAt.HasValue && x.Subscription.EndsAt.HasValue,
                    () =>
                    {
                        RuleFor(x => x.Subscription!)
                            .Must(s => s.EndsAt > s.StartsAt)
                            .WithMessage(
                                "La fecha de fin de la suscripción debe ser posterior a la de inicio.");
                    });
            });
        }
    }
}
