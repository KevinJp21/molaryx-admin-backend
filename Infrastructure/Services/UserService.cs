using Application.Common.Interfaces;
using Domain.Contracts;
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

        public async Task<User> CreatePendingOwnerAsync<TOwnerRegistration>(
            TOwnerRegistration dto,
            long idTenant,
            CancellationToken cancellationToken
        ) where TOwnerRegistration : IOwnerRegistration
        {

            var username = dto.Username.Trim().ToLowerInvariant();

            var usernameExists = await _unitOfWork.UserRepository.ExistsByUsernameAsync(
                username,
                cancellationToken
            );

            if (usernameExists)
            {
                throw new InvalidOperationException("El nombre de usuario ya se encuentra registrado.");
            }

            var email = dto.Email.Trim().ToLowerInvariant();

            var emailExists = await _unitOfWork.UserRepository.ExistsByEmailAsync(
                email,
                cancellationToken
            );

            if (emailExists)
            {
                throw new InvalidOperationException("El correo electrónico del usuario ya se encuentra registrado.");
            }

            if (!string.IsNullOrWhiteSpace(dto.IdentificationNumber))
            {
                var identificationNumberExists = await _unitOfWork
                    .UserRepository.ExistsByIdentificationNumberAsync(
                        dto.IdentificationNumber,
                        cancellationToken);

                if (identificationNumberExists)
                {
                    throw new InvalidOperationException("El número de identificación del usuario ya se encuentra registrado.");
                }
            }

            var phoneNumberExists = await _unitOfWork.UserRepository.ExistsByPhoneNumberAsync(
                dto.PhoneNumber,
                cancellationToken
            );

            if (phoneNumberExists)
            {
                throw new InvalidOperationException("El número de telefono del usuario ya se encuentra registrado.");
            }

            var salt = _hasherService.GenerateSalt();

            var hashedPassword = _hasherService.ComputeHashBytes(
                dto.Password,
                salt
            );

            var user = new User
            {
                IdTenant = idTenant,

                IdUserStatus =
                    (short)UserStatusEnum.PENDING,

                IdUserRole =
                    (short)UserRoleEnum.OWNER,

                Username = dto.Username,
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,

                FirstSurname = dto.FirstSurname,
                SecondSurname = dto.SecondSurname,

                IdIdentificationType = dto.IdIdentificationType,
                IdentificationNumber = dto.IdentificationNumber,

                PhoneNumber = dto.PhoneNumber,
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

        public async Task<User> ActivateUserAsync(
            long idUser,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(idUser, cancellationToken);

            if (user is null)
            {
                throw new InvalidOperationException("El usuario no existe.");
            }

            if (user.IdUserStatus != (short)UserStatusEnum.PENDING)
            {
                throw new InvalidOperationException("El usuario no se encuentra pendiente de activación.");
            }

            user.IdUserStatus = (short)UserStatusEnum.ACTIVE;

            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

            return user;
        }
    }
}