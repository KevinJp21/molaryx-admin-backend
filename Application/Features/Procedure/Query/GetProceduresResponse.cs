namespace Application.Features.Procedure.Query
{
    public class GetProceduresResponse
    {
        public long IdProcedure { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? ReferencePrice { get; set; }
        public bool IsActive { get; set; }
    }
}
