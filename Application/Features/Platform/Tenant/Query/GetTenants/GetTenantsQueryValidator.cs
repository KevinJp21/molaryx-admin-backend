using Domain.Common.Tenants;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Platform.Tenant.Query.GetTenants
{
    public class GetTenantsQueryValidator : AbstractValidator<GetTenantsQuery>
    {
        public GetTenantsQueryValidator()
        {
            When(x => !string.IsNullOrWhiteSpace(x.Search), () =>
            {
                RuleFor(x => x.Search)
                    .Must(TenantSearch.HasValidSearch)
                    .WithMessage(
                        $"Ingresa al menos {TenantSearch.MinTokenLength} caracteres para buscar.");
            });

            When(x => x.IdTenantStatus.HasValue, () =>
            {
                RuleFor(x => x.IdTenantStatus)
                    .Must(status => Enum.IsDefined((TenantStatusEnum)status!.Value))
                    .WithMessage("El estado del tenant no es válido.");
            });
        }
    }
}
