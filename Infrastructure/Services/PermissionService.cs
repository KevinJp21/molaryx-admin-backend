using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class PermissionService(IUnitOfWork unitOfWork) : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> RoleHasPermissionAsync(
            short idUserRole,
            string permissionCode,
            CancellationToken cancellationToken = default
        )
        {
            return await _unitOfWork.RolePermissionRepository.ExistsAsync(
                RolePermissionSpec.ByRoleAndCode(idUserRole, permissionCode),
                cancellationToken
            );
        }
    }
}