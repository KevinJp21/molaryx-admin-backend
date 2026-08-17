namespace Application.Features.Treatment.Query.GetTreatments
{
    public class GetTreatmentsResponse
    {
        public long IdTreatment { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
