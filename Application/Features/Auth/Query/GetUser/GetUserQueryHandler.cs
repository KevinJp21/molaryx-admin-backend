using Application.Common.Mediator.Interfaces;
using Application.Context;
using Application.DTOs.Users;
using Domain.Contracts;
using Domain.Contracts.IRepositories;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser) : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync((long)_currentUser.IdUser!, cancellationToken)
                ?? throw new Exception("Usuario no encontrado.");

            var mapperResult = new UserDto
            {
                IdUser = user.IdUser,
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
                Name = $"{user.FirstName} {user.FirstSurname}",
                Email = user.Email,
                
            };

            return mapperResult;
        }
    }
    
}