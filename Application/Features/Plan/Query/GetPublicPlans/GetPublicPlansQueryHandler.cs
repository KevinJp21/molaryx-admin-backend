using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IRepositories;
using Domain.Exceptions;
//todo revisar el error de sessions al aplciar un filtro
namespace Application.Features.Plan.Query.GetPublicPlans
{
    public class GetPublicPlansQueryHandler(IPlanRepository _planRepository)
        : IRequestHandler<GetPublicPlansQuery, List<GetPublicPlansQueryResponse>>
    {
        public async Task<List<GetPublicPlansQueryResponse>> Handle(
            GetPublicPlansQuery request,
            CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetPublicPlansAsync(cancellationToken);

            if (plans.Count == 0)
                throw new NotFoundException("No se encontraron planes disponibles, intente nuevamente más tarde.");

            return [.. plans.Select(p =>
            {
                var promotionPlan = p.PromotionPlans.FirstOrDefault();

                return new GetPublicPlansQueryResponse
                {
                    IdPlan = p.IdPlan,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    MaxProfessionals = p.MaxProfessionals,
                    MaxAssistants = p.MaxAssistants,
                    MaxPatients = p.MaxPatients,
                    PromotionPlan = promotionPlan is null
                        ? null
                        : new PromotionPlanResponse
                        {
                            IdPromotion = promotionPlan.IdPromotion,
                            PromotionName = promotionPlan.Promotion.Name,
                            Price = promotionPlan.Price
                        }
                };
            })];
        }
    }
}
