using Domain.Common;
using FluentValidation;

namespace Application.Features.Procedure.Query
{
    public class GetProceduresQueryValidator : AbstractValidator<GetProceduresQuery>
    {
        public GetProceduresQueryValidator()
        {
            When(x => !string.IsNullOrWhiteSpace(x.Search), () =>
            {
               RuleFor(x => x.Search).Must(Search.HasValidSearch).WithMessage($"Ingresa al menos {Search.MinTokenLength} caracteres para buscar.");
            });
        }
    }
}