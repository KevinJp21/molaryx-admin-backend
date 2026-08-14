using Application.Features.Service.Command.CreateService;
using Application.Features.Service.Command.UpdateService;

namespace Domain.Contracts.IServices
{
    public interface IServiceService
    {
        Task<bool> CreateServiceAsync(CreateServiceCommand request, CancellationToken cancellationToken);
        Task<bool> UpdateServiceAsync(UpdateServiceCommand request, CancellationToken cancellationToken);
        Task<bool> DeleteServiceAsync(long idService, CancellationToken cancellationToken);
    }
}