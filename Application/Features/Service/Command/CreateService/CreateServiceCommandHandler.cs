using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Service.Command.CreateService
{
    public class CreateServiceCommandHandler(
        IServiceService _serviceService
    ) : IRequestHandler<CreateServiceCommand, bool>
    {
        public async Task<bool> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            return await _serviceService.CreateServiceAsync(request, cancellationToken);
        }
    }
}
