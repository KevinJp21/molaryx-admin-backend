using Application.Common.Mediator.Interfaces;
using Application.DTOs;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Exceptions;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler(IUserRepository userRepository, IHasherService hasherService, ITokenService tokenService) : IRequestHandler<LoginCommand, LoginDTO>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHasherService _hasherService = hasherService;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<LoginDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken)
                ?? throw new NotFoundException("Usuario o contraseña invalida.");

            var hashedPassword = _hasherService.ComputeHashBytes(request.Password, user.Salt);

            if (!hashedPassword.SequenceEqual(user.Password))
            {
                throw new InvalidCredentialsException("Usuario o contraseña invalida.");
            }

            var token = _tokenService.GenerateToken(user.IdUser, user.IdUserRole, user.Email, DateTime.UtcNow.AddHours(1));

            var response = new LoginDTO { AuthToken = token };

            return response;
        }
    }
}