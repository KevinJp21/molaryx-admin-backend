using Application.Common.Interfaces;
using Application.Features.Users.Command.CreateMember;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class UserService(
        IUnitOfWork _unitOfWork,
        IHasherService _hasherService,
        ITenantAccessService _tenantAccessService
    ) : IUserService
    {
        private readonly IHasherService _hasherService = _hasherService;

        private readonly IUnitOfWork _unitOfWork = _unitOfWork;

        public async Task<User> CreatePendingOwnerAsync<TOwnerRegistration>(
            TOwnerRegistration dto,
            long idTenant,
            CancellationToken cancellationToken
        ) where TOwnerRegistration : IOwnerRegistration
        {

            var username = dto.Username.Trim().ToLowerInvariant();

            var usernameExists = await _unitOfWork.UserRepository.ExistsAsync(
                UserSpec.ByUsername(username),
                cancellationToken
            );

            if (usernameExists)
            {
                throw new InvalidOperationException("El nombre de usuario ya se encuentra registrado.");
            }

            var email = dto.Email.Trim().ToLowerInvariant();

            var emailExists = await _unitOfWork.UserRepository.ExistsAsync(
                UserSpec.ByEmail(email),
                cancellationToken
            );

            if (emailExists)
            {
                throw new InvalidOperationException("El correo electrónico del usuario ya se encuentra registrado.");
            }

            if (!string.IsNullOrWhiteSpace(dto.IdentificationNumber))
            {
                var identificationNumberExists = await _unitOfWork
                    .UserRepository.ExistsAsync(
                        UserSpec.ByIdentificationNumber(dto.IdentificationNumber),
                        cancellationToken);

                if (identificationNumberExists)
                {
                    throw new InvalidOperationException("El número de identificación del usuario ya se encuentra registrado.");
                }
            }

            var phoneNumberExists = await _unitOfWork.UserRepository.ExistsAsync(
                UserSpec.ByPhoneNumber(dto.PhoneNumber),
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
                BirthDate = dto.BirthDate,

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

        public async Task<(bool Success, string TemporaryPassword, string ConsultoryName)> CreateMemberAsync(
            CreateMemberCommand command,
            CancellationToken cancellationToken
        )
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            if (command.IdUserRole != (short)UserRoleEnum.PROFESSIONAL && command.IdUserRole != (short)UserRoleEnum.ASSISTANT)
            {
                throw new InvalidOperationException("El rol de usuario no es válido.");
            }

            var userCount = await _unitOfWork.UserRepository.CountAsync(UserSpec.ForTeamCount(access.IdTenant, command.IdUserRole), cancellationToken);

            if (command.IdUserRole == (short)UserRoleEnum.PROFESSIONAL)
            {
                if (access.Subscription.MaxProfessionals is short maxProfessionals && userCount >= maxProfessionals)
                {
                    throw new InvalidOperationException("El consultorio ha alcanzado el número máximo de profesionales.");
                }
            }
            else if (command.IdUserRole == (short)UserRoleEnum.ASSISTANT)
            {
                if (access.Subscription.MaxAssistants is short maxAssistants && userCount >= maxAssistants)
                {
                    throw new InvalidOperationException("El consultorio ha alcanzado el número máximo de asistentes.");
                }
            }

            var usernameExists = await _unitOfWork.UserRepository.ExistsAsync(UserSpec.ByUsername(command.Username), cancellationToken);

            if (usernameExists)
            {
                throw new InvalidOperationException("El nombre de usuario ya se encuentra registrado.");
            }

            var identificationNumberExists = await _unitOfWork.UserRepository.ExistsAsync(UserSpec.ByIdentificationNumber(command.IdentificationNumber), cancellationToken);

            if (identificationNumberExists)
            {
                throw new InvalidOperationException("El número de identificación ya se encuentra registrado.");
            }

            var phoneNumberExists = await _unitOfWork.UserRepository.ExistsAsync(UserSpec.ByPhoneNumber(command.PhoneNumber), cancellationToken);

            if (phoneNumberExists)
            {
                throw new InvalidOperationException("El número de teléfono ya se encuentra registrado.");
            }

            var emailExists = await _unitOfWork.UserRepository.ExistsAsync(UserSpec.ByEmail(command.Email), cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException("El correo electrónico ya se encuentra registrado.");
            }

            var salt = _hasherService.GenerateSalt();

            var password = $"{command.IdentificationNumber}{command.Username}@Molaryx";

            var hashedPassword = _hasherService.ComputeHashBytes(
                password,
                salt
            );

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = new User
                {
                    IdUserStatus = (short)UserStatusEnum.ACTIVE,
                    IdUserRole = command.IdUserRole,
                    IdTenant = access.IdTenant,
                    Username = command.Username.Trim(),
                    FirstName = command.FirstName.Trim(),
                    SecondName = command.SecondName?.Trim(),
                    FirstSurname = command.FirstSurname.Trim(),
                    SecondSurname = command.SecondSurname?.Trim(),
                    IdIdentificationType = command.IdIdentificationType,
                    IdentificationNumber = command.IdentificationNumber,
                    BirthDate = command.BirthDate,
                    PhoneNumber = command.PhoneNumber,
                    Email = command.Email.Trim().ToLowerInvariant(),
                    Password = hashedPassword,
                    Salt = salt
                };
                await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);
                await _unitOfWork.UserRepository.SaveChangesAsync(cancellationToken);

                if (user.IdUserRole == (short)UserRoleEnum.PROFESSIONAL)
                {
                    var professional = new Professional
                    {
                        IdTenant = access.IdTenant,
                        IdUser = user.IdUser
                    };

                    await _unitOfWork.ProfessionalRepository.AddAsync(professional, cancellationToken);
                }

                if (command.IdUserRole == (short)UserRoleEnum.ASSISTANT)
                {
                    var assistant = new Assistant
                    {
                        IdTenant = access.IdTenant,
                        IdUser = user.IdUser
                    };

                    await _unitOfWork.AssistantRepository.AddAsync(assistant, cancellationToken);
                }

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return (true, password, access.Tenant.ConsultoryName);
            }
            catch
            {
                if (_unitOfWork.IsInTransaction)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                }

                throw;
            }
        }
    }
}