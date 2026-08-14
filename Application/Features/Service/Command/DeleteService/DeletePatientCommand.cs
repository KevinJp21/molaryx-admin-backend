using Application.Common.Mediator.Interfaces;

namespace Application.Features.Service.Command.DeleteService
{
    public class DeleteServiceCommand : IRequest<bool>
    {
        public long IdService { get; set; }
    }
}