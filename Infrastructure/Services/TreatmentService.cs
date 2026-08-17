using Application.Features.Treatment.Command.CreateTreatment;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class TreatmentService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : ITreatmentService
    {
        public async Task<bool> CreateTreatmentAsync(
            CreateTreatmentCommand request,
            CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var nameExists = await _unitOfWork.TreatmentRepository.ExistsAsync(
                TreatmentsSpec.ByName(access.IdTenant, request.Name.Trim()),
                cancellationToken);

            if (nameExists)
            {
                throw new InvalidOperationException("Ya existe un tratamiento con ese nombre.");
            }

            var treatment = new Treatment
            {
                IdTenant = access.IdTenant,
                Name = request.Name.Trim(),
                Description = string.IsNullOrWhiteSpace(request.Description)
                    ? null
                    : request.Description.Trim(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.TreatmentRepository.AddAsync(treatment, cancellationToken);
            await _unitOfWork.TreatmentRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
