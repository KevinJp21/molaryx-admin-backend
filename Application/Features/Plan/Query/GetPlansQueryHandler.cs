using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IRepositories;
using Domain.Exceptions;
using Domain.Specifications;

namespace Application.Features.Plan.Query
{
    public class GetPlansQueryHandler(IPlanRepository _planRepository) : IRequestHandler<GetPlansQuery, List<GetPlansQueryResponse>>
    {
        public async Task<List<GetPlansQueryResponse>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetAll(new PublicPlansSpecification(), cancellationToken) ?? throw new NotFoundException("No se encontraron planes disponibles.");

            return [.. plans.Select(p => new GetPlansQueryResponse
            {
                IdPlan = p.IdPlan,
                Name = p.Name,
                Description = p.Description ?? string.Empty,
                Price = p.Price!.Value,
                MaxProfessionals = p.MaxProfessionals!.Value,
                MaxAssistants = p.MaxAssistants!.Value,
                MaxPatients = (short)p.MaxPatients!.Value,
                PromotionPlans = [.. p.PromotionPlans.Select(pp => new PromotionPlanResponse
                {
                    IdPromotion = pp.IdPromotion,
                    PromotionName = pp.Promotion.Name,
                    Price = pp.Price
                })]
            })];
        }
    }
}
