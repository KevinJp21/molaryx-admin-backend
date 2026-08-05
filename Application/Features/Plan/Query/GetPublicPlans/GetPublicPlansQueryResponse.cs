namespace Application.Features.Plan.Query.GetPublicPlans
{
    public class GetPublicPlansQueryResponse
    {
        public short IdPlan { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public short? MaxProfessionals { get; set; }
        public short? MaxAssistants { get; set; }
        public int? MaxPatients { get; set; }
        public List<PromotionPlanResponse> PromotionPlans { get; set; } = [];
    }

    public class PromotionPlanResponse
    {
        public long IdPromotion { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
