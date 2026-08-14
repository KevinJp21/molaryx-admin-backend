using Application.Common.Mediator.Interfaces;
using Application.Features.Service.Command.DeleteService;
using Domain.Contracts.IServices;

namespace Application.Features.Service.Command.DeleteService
{
    public class DeleteServiceCommandHandler(
        IServiceService _serviceService
    ) : IRequestHandler<DeleteServiceCommand, bool>
    {
        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            return await _serviceService.DeleteServiceAsync(request.IdService, cancellationToken);
        }
    }
}
