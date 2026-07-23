using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Exceptions;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<loginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasherService _hasherService;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserRepository userRepository, IHasherService hasherService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _hasherService = hasherService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(loginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken)
                ?? throw new NotFoundException("Usuario no encontrado");

            var hashedPassword = _hasherService.ComputeHashBytes(request.Password, user.Salt);

            if (!hashedPassword.SequenceEqual(user.Password))
            {
                throw new InvalidCredentialsException("Usuario o contraseña invalida.");
            }

            var token = _tokenService.GenerateToken(user.IdUser, user.Email, DateTime.UtcNow.AddHours(1));

            var response = new LoginResponse { Token = token };

            return response;
        }
    }
}