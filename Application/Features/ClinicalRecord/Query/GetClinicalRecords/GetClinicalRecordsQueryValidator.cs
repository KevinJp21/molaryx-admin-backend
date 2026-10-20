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

            When(x => x.From.HasValue && x.To.HasValue, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.From <= x.To)
                    .WithMessage("La fecha inicial no puede ser posterior a la fecha final.");
            });
        }
    }
}
