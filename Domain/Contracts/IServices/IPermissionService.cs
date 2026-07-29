namespace Domain.Contracts.IServices
{
    public interface IPermissionService
    {
        Task<bool> RoleHasPermissionAsync(
            short idUserRole,
            string permissionCode,
            CancellationToken cancellationToken = default
        );
    }
}