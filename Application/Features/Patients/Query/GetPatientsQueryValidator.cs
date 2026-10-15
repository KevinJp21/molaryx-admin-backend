using Domain.Common;
using FluentValidation;

namespace Application.Features.Patients.Query
{
    public class GetPatientsQueryValidator : AbstractValidator<GetPatientsQuery>
    {
        public GetPatientsQueryValidator()
        {
            When(x => !string.IsNullOrWhiteSpace(x.Search), () =>
            {
                RuleFor(x => x.Search)
                    .Must(Search.HasValidSearch)
                    .WithMessage(
                        $"Ingresa al menos {Search.MinTokenLength} caracteres para buscar.");
            });
        }
    }
}
