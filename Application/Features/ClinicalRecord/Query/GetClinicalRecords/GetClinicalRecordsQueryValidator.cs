using Domain.Common.ClinicalRecords;
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
                    .Must(ClinicalRecordSearch.HasValidSearch)
                    .WithMessage(
                        $"Ingresa al menos {ClinicalRecordSearch.MinTokenLength} caracteres para buscar.");
            });
        }
    }
}
