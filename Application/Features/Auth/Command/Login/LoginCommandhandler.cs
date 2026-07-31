using Application.Common.Mediator.Interfaces;
using Application.DTOs.Auth;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork, IHasherService hasherService, ISessionService sessionService) : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IHasherService _hasherService = hasherService;
        private readonly ISessionService _sessionService = sessionService;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken)
                ?? throw new InvalidCredentialsException("Usuario o contraseña invalida.");


            var hashedPassword = _hasherService.ComputeHashBytes(request.Password, user.Salt);

            if (!hashedPassword.SequenceEqual(user.Password))
            {
                throw new InvalidCredentialsException("Usuario o contraseña invalida.");
            }

            switch (user.IdUserStatus)
            {
                case (short)UserStatusEnum.PENDING:
                    throw new InvalidOperationException("La cuenta se encuentra pendiente de activación.");

                case (short)UserStatusEnum.BLOCKED:
                    throw new InvalidOperationException("La cuenta se encuentra bloqueada.");

                case (short)UserStatusEnum.INACTIVE:
                    throw new InvalidOperationException("La cuenta se encuentra inactiva.");
            }

            switch (user.Tenant.IdTenantStatus)
            {
                case (short)TenantStatusEnum.BLOCKED:
                    throw new InvalidOperationException("El consultorio se encuentra bloqueado.");

                case (short)TenantStatusEnum.INACTIVE:
                    throw new InvalidOperationException("El consultorio se encuentra inactivo.");

                case (short)TenantStatusEnum.PENDING:
                    throw new InvalidOperationException("El consultorio se encuentra pendiente de activación.");

                case (short)TenantStatusEnum.REJECTED:
                    throw new InvalidOperationException("El consultorio ha sido rechazado.");
            }

            var (AuthToken, refreshToken) = await _sessionService.CreateSessionAsync(
                user.IdUser,
                user.IdUserRole,
                user.IdTenant,
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