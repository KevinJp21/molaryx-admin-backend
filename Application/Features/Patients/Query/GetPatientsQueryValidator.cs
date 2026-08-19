using Domain.Common.Patients;
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
                    .Must(PatientSearch.HasValidSearch)
                    .WithMessage(
                        $"Ingresa al menos {PatientSearch.MinTokenLength} caracteres para buscar.");
            });
        }
    }
}
