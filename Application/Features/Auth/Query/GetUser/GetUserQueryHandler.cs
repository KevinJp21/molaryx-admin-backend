using Application.Common.Mediator.Interfaces;
using Application.Context;
using Domain.Contracts;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser) : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync((long)_currentUser.IdUser!, cancellationToken)
                ?? throw new Exception("Usuario no encontrado.");

            var permissions = await _unitOfWork.UserRepository.GetPermissionsByUserIdAsync((long)_currentUser.IdUser!, cancellationToken);

            var mapperResult = new GetUserQueryResponse
            {
                Role = new UserRole
                {
                    IdUserRole = user.UserRole.IdUserRole,
                    Name = user.UserRole.Name
                },
                IdTenant = user.IdTenant,
                Status = new UserStatus
                {
                    IdUserStatus = user.UserStatus.IdUserStatus,
                    Name = user.UserStatus.Name
                },
                Username = user.Username,
                Names = $"{user.FirstName}{(!string.IsNullOrEmpty(user.SecondName) ? $" {user.SecondName}" : string.Empty)}",
                Surnames = $"{user.FirstSurname}{(!string.IsNullOrEmpty(user.SecondSurname) ? $" {user.SecondSurname}" : string.Empty)}",
                Email = user.Email,
                Permissions = [.. permissions
                    .GroupBy(p => p.Module.Code)
                    .Select(g => new ModulePermissions
                    {
                        Module = g.Key,
                        Codes = [.. g.Select(p => p.Code)]
                    })]
            };

            return mapperResult;
        }
    }
}