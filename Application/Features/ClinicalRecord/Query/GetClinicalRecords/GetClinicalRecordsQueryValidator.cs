using Domain.Common;
using FluentValidation;

namespace Application.Features.ClinicalRecord.Query.GetClinicalRecords
{
    public class GetClinicalRecordsQueryValidator : AbstractValidator<GetClinicalRecordsQuery>
    {
        public GetClinicalRecordsQueryValidator()
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
