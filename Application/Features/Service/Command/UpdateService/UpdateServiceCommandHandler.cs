using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Service.Command.UpdateService
{
    public class UpdateServiceCommandHandler(
        IServiceService _serviceService
    ) : IRequestHandler<UpdateServiceCommand, bool>
    {
        public async Task<bool> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            return await _serviceService.UpdateServiceAsync(request, cancellationToken);
        }
    }
}
