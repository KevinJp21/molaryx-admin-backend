using Application.DTOs.Tenant.TenantRegistration;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class UserService(
        IUserRepository userRepository,
        IHasherService hasherService
    ) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHasherService _hasherService = hasherService;

        public async Task<User> CreatePendingOwnerAsync(
            OwnerRegistrationDto dto,
            long idTenant,
            CancellationToken cancellationToken
        )
        {
            var salt = _hasherService.GenerateSalt();

            var hashedPassword = _hasherService.ComputeHashBytes(
                dto.Password,
                salt
            );

            var user = new User
            {
                IdTenant = idTenant,

                IdUserStatus =
                    (short)UserStatusEnum.PENDING_APPROVAL,

                IdUserRole =
                    (short)UserRoleEnum.OWNER,

                Username = dto.Username,
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,

                FirstSurname = dto.FirstSurname,
                SecondSurname = dto.SecondSurname,

                IdIdentificationType = dto.IdIdentificationType,
                IdentificationNumber = dto.IdentificationNumber,

                Phone = dto.Phone,
                Email = dto.Email,

                Password = hashedPassword,
                Salt = salt
            };

            await _userRepository.AddAsync(
                user,
                cancellationToken
            );

            return user;
        }
    }
}