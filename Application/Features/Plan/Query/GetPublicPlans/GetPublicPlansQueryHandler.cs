using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IRepositories;
using Domain.Exceptions;
using Domain.Specifications;

namespace Application.Features.Plan.Query.GetPublicPlans
{
    public class GetPublicPlansQueryHandler(IPlanRepository _planRepository)
        : IRequestHandler<GetPublicPlansQuery, List<GetPublicPlansQueryResponse>>
    {
        public async Task<List<GetPublicPlansQueryResponse>> Handle(
            GetPublicPlansQuery request,
            CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetAll(new PublicPlansSpecification(), cancellationToken)
                ?? throw new NotFoundException("No se encontraron planes disponibles.");

            return [.. plans.Select(p => new GetPublicPlansQueryResponse
            {
                IdPlan = p.IdPlan,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                MaxProfessionals = p.MaxProfessionals,
                MaxAssistants = p.MaxAssistants,
                MaxPatients = p.MaxPatients,
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
