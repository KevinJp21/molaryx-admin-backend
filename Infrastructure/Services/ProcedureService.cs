using Application.Features.Procedure.Command.CreateProcedure;
using Application.Features.Procedure.Command.UpdateProcedure;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class ProcedureService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IProcedureService
    {
        public async Task<bool> CreateProcedureAsync(
            CreateProcedureCommand request,
            CancellationToken cancellationToken
        )
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var procedure = new Procedure
            {
                IdTenant = access.IdTenant,
                Name = request.Name.Trim(),
                Description = request.Description,
                ReferencePrice = request.ReferencePrice,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ProcedureRepository.AddAsync(procedure, cancellationToken);
            await _unitOfWork.ProcedureRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> UpdateProcedureAsync(
            UpdateProcedureCommand request,
            CancellationToken cancellationToken
        )
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var procedure = await _unitOfWork.ProcedureRepository.GetByIdAsync(
                request.IdProcedure,
                cancellationToken,
                ProceduresSpec.ById(request.IdProcedure)
            ) ?? throw new NotFoundException("El procedimiento no existe.");

            if (request.Name is not null && request.Name.Trim() != procedure.Name)
            {
                var nameExists = await _unitOfWork.ProcedureRepository.ExistsAsync(
                    ProceduresSpec.ByName(access.IdTenant, request.Name.Trim()),
                    cancellationToken);

                if (nameExists)
                {
                    throw new InvalidOperationException("Ya existe un procedimiento con ese nombre.");
                }
            }

            if (procedure.IdTenant != access.IdTenant)
            {
                throw new InvalidOperationException("El procedimiento no pertenece a este consultorio.");
            }

            procedure.Name = request.Name ?? procedure.Name;
            procedure.Description = request.Description ?? procedure.Description;
            if (request.ReferencePrice.HasValue)
            {
                procedure.ReferencePrice = request.ReferencePrice.Value <= 0
                    ? null
                    : request.ReferencePrice;
            }
            procedure.IsActive = request.IsActive ?? procedure.IsActive;
            procedure.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.ProcedureRepository.UpdateAsync(procedure, cancellationToken);
            await _unitOfWork.ProcedureRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteProcedureAsync(long idProcedure, CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var procedure = await _unitOfWork.ProcedureRepository.GetByIdAsync(
                idProcedure,
                cancellationToken,
                ProceduresSpec.ById(idProcedure)
            )
                ?? throw new NotFoundException("El procedimiento no existe.");

            if (procedure.IdTenant != access.IdTenant)
            {
                throw new InvalidOperationException("El procedimiento no pertenece a este consultorio.");
            }

            procedure.DeletedAt = DateTime.UtcNow;
            procedure.IsActive = false;

            await _unitOfWork.ProcedureRepository.UpdateAsync(procedure, cancellationToken);
            await _unitOfWork.ProcedureRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
