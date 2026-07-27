using Application.Common.Mediator.Interfaces;
using Application.Context;
using Application.DTOs.Users;
using Domain.Contracts.IRepositories;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserQueryHandler(IUserRepository userRepository, ICurrentUser currentUser) : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync((long)_currentUser.IdUser!, cancellationToken)
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