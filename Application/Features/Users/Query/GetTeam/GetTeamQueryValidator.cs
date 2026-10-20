using Domain.Common;
using FluentValidation;

namespace Application.Features.Users.Query.GetTeam
{
    public class GetTeamQueryValidator : AbstractValidator<GetTeamQuery>
    {
        public GetTeamQueryValidator()
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