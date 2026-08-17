using Application.Features.Treatment.Command.CreateTreatment;
using Application.Features.Treatment.Command.UpdateTreatment;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;
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

        public async Task<bool> UpdateTreatmentAsync(
            UpdateTreatmentCommand request,
            CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var treatment = await _unitOfWork.TreatmentRepository.GetByIdAsync(
                request.IdTreatment, cancellationToken, TreatmentsSpec.ById(request.IdTreatment))
                ?? throw new NotFoundException("El tratamiento no existe.");

            if (request.Name is not null && request.Name.Trim() != treatment.Name)
            {
                var nameExists = await _unitOfWork.TreatmentRepository.ExistsAsync(
                    TreatmentsSpec.ByName(access.IdTenant, request.Name.Trim()),
                    cancellationToken);

                if (nameExists)
                {
                    throw new InvalidOperationException("Ya existe un tratamiento con ese nombre.");
                }
            }

            if (treatment.IdTenant != access.IdTenant)
            {
                throw new InvalidOperationException("El tratamiento no pertenece a este consultorio.");
            }

            treatment.Name = request.Name?.Trim() ?? treatment.Name;
            treatment.Description = request.Description?.Trim() ?? treatment.Description;
            treatment.IsActive = request.IsActive ?? treatment.IsActive;
            treatment.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.TreatmentRepository.UpdateAsync(treatment, cancellationToken);
            await _unitOfWork.TreatmentRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteTreatmentAsync(long idTreatment, CancellationToken cancellationToken)
        {

            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var treatment = await _unitOfWork.TreatmentRepository.GetByIdAsync(
                idTreatment,
                cancellationToken,
                TreatmentsSpec.ById(idTreatment)
            )
                ?? throw new NotFoundException("El tratamiento no existe.");

            if (treatment.IdTenant != access.IdTenant)
            {
                throw new InvalidOperationException("El tratamiento no pertenece a este consultorio.");
            }

            treatment.DeletedAt = DateTime.UtcNow;
            treatment.IsActive = false;

            await _unitOfWork.TreatmentRepository.UpdateAsync(treatment, cancellationToken);
            await _unitOfWork.TreatmentRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
