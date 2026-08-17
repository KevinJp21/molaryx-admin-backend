using Application.Features.Service.Command.CreateService;
using Application.Features.Service.Command.UpdateService;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class ServiceService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IServiceService
    {
        public async Task<bool> CreateServiceAsync(
            CreateServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var service = new Service
            {
                IdTenant = access.IdTenant,
                Name = request.Name.Trim(),
                Description = request.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ServiceRepository.AddAsync(service, cancellationToken);
            await _unitOfWork.ServiceRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> UpdateServiceAsync(
            UpdateServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(
                request.IdService,
                cancellationToken,
                ServicesSpec.ById(request.IdService)
            ) ?? throw new NotFoundException("El servicio no existe.");

            if (request.Name is not null && request.Name.Trim() != service.Name)
            {
                var nameExists = await _unitOfWork.ServiceRepository.ExistsAsync(
                    ServicesSpec.ByName(access.IdTenant, request.Name.Trim()),
                    cancellationToken);

                if (nameExists)
                {
                    throw new InvalidOperationException("Ya existe un servicio con ese nombre.");
                }
            }

            if (service.IdTenant != access.IdTenant)
            {
                throw new InvalidOperationException("El servicio no pertenece a este consultorio.");
            }

            service.Name = request.Name ?? service.Name;
            service.Description = request.Description ?? service.Description;
            service.IsActive = request.IsActive ?? service.IsActive;
            service.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.ServiceRepository.UpdateAsync(service, cancellationToken);
            await _unitOfWork.ServiceRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteServiceAsync(long idService, CancellationToken cancellationToken)
        {

            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(
                idService,
                cancellationToken,
                ServicesSpec.ById(idService)
            )
                ?? throw new NotFoundException("El servicio no existe.");

            if (service.IdTenant != access.IdTenant)
            {
                throw new InvalidOperationException("El servicio no pertenece a este consultorio.");
            }

            service.DeletedAt = DateTime.UtcNow;
            service.IsActive = false;

            await _unitOfWork.ServiceRepository.UpdateAsync(service, cancellationToken);
            await _unitOfWork.ServiceRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
