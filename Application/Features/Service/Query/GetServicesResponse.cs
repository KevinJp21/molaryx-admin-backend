namespace Application.Features.Service.Query
{
    public class GetServicesResponse
    {
        public long IdService { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
