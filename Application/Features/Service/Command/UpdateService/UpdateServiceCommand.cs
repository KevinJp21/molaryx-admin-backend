using Application.Common.Mediator.Interfaces;

namespace Application.Features.Service.Command.UpdateService
{
    public class UpdateServiceCommand : IRequest<bool>
    {
        public long IdService { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}