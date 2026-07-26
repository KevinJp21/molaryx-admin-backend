using Application.Common.Mediator.Interfaces;
using Application.DTOs.Auth;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Exceptions;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler(IUserRepository userRepository, IHasherService hasherService, ISessionService sessionService) : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHasherService _hasherService = hasherService;
        private readonly ISessionService _sessionService = sessionService;

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken)
                ?? throw new NotFoundException("Usuario o contraseña invalida.");

            var hashedPassword = _hasherService.ComputeHashBytes(request.Password, user.Salt);

            if (!hashedPassword.SequenceEqual(user.Password))
            {
                throw new InvalidCredentialsException("Usuario o contraseña invalida.");
            }

            var ( AuthToken, refreshToken ) = await _sessionService.CreateSessionAsync(
                user.IdUser,
                user.IdUserRole,
                user.Email,
                cancellationToken
            );

            var response = new LoginResponseDto
            {
                AuthToken = AuthToken,
                RefreshToken = refreshToken
            };

            return response;
        }
    }
}