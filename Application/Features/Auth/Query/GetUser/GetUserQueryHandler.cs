using Application.Common.Mediator.Interfaces;
using Application.Context;
using Domain.Contracts.IRepositories;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserQueryHandler(IUserRepository userRepository, ICurrentUser currentUser) : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
                throw new UnauthorizedAccessException("Usuario no autenticado.");

            var user = await _userRepository.GetByIdAsync((long)_currentUser.UserId!, cancellationToken)
                ?? throw new Exception("Usuario no encontrado.");

            var mapperResult = new GetUserResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                FirstSurname = user.FirstSurname
            };

            return mapperResult;
        }
    }
    
}