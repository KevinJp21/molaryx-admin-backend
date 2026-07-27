using Microsoft.AspNetCore.Authorization;

namespace Application.Common.Authorization
{
    public class PermissionRequirement(string permissionCode) : IAuthorizationRequirement
    {
        public string PermissionCode { get; } = permissionCode;
    }
}