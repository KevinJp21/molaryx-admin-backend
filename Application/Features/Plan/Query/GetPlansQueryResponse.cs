namespace Application.Features.Plan.Query
{
    public class GetPlansQueryResponse
    {
        public short IdPlan { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public short MaxProfessionals { get; set; }
        public short MaxAssistants { get; set; }
        public short MaxPatients { get; set; }
        public List<PromotionPlanResponse> PromotionPlans { get; set; } = [];
    }

    public class PromotionPlanResponse
    {
        public long IdPromotion { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
