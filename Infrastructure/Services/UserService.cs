using Application.DTOs.Tenant.TenantRegistration;
using Domain.Contracts;
using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class UserService(
        IUnitOfWork unitOfWork,
        IHasherService hasherService
    ) : IUserService
    {
        private readonly IHasherService _hasherService = hasherService;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

            await _unitOfWork.UserRepository.AddAsync(
                user,
                cancellationToken
            );

            return user;
        }
    }
}