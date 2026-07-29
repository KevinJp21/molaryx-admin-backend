using Application.Common.Authorization;
using Application.Context;
using Domain.Contracts.IServices;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization
{
    public class PermissionAuthorizationHandler(
        IPermissionService permissionService,
        ICurrentUser currentUser
    ) : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService =
            permissionService;

        private readonly ICurrentUser _currentUser =
            currentUser;

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (_currentUser.IdUserRole is null)
            {
                return;
            }

            var idUserRole = _currentUser.IdUserRole;

            if (idUserRole is null)
            {
                return;
            }

            // SUPERADMIN tiene acceso a todo el sistema
            if (idUserRole == (short)UserRoleEnum.SUPER_ADMIN)
            {
                context.Succeed(requirement);
                return;
            }

            var hasPermission =
                await _permissionService.RoleHasPermissionAsync(
                    idUserRole.Value,
                    requirement.PermissionCode
                );

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}