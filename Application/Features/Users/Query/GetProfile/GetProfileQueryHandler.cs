using Application.Common.Mediator.Interfaces;
using Application.Context;
using Domain.Contracts;
using Domain.Enums;
using Domain.Specifications;

namespace Application.Features.Users.Query.GetProfile
{
    public class GetProfileQueryHandler(
        IUnitOfWork _unitOfWork,
        ICurrentUser _currentUser
    ) : IRequestHandler<GetProfileQuery, GetProfileQueryResponse>
    {
        public async Task<GetProfileQueryResponse> Handle(
            GetProfileQuery request,
            CancellationToken cancellationToken)
        {
            var idUser = _currentUser.IdUser!.Value;

            var user = await _unitOfWork.UserRepository.GetByIdAsync(
                idUser,
                cancellationToken,
                new UserSpec(idUser))
                ?? throw new InvalidOperationException("Usuario no encontrado.");

            var response = new GetProfileQueryResponse
            {
                IdUser = user.IdUser,
                Username = user.Username,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                FirstSurname = user.FirstSurname,
                SecondSurname = user.SecondSurname,
                IdIdentificationType = user.IdIdentificationType,
                IdentificationType = user.IdentificationType.Name,
                IdentificationNumber = user.IdentificationNumber,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IdUserRole = user.IdUserRole,
                RoleName = user.UserRole.Name,
                IdUserStatus = user.IdUserStatus,
                StatusName = user.UserStatus.Name
            };

            if (user.IdUserRole == (short)UserRoleEnum.OWNER && user.Tenant is not null)
            {
                response.Tenant = new ProfileTenantSummary
                {
                    ConsultoryName = user.Tenant.ConsultoryName,
                    Email = user.Tenant.Email,
                    PhoneNumber = user.Tenant.PhoneNumber,
                    Address = user.Tenant.Address,
                    IdIdentificationType = user.Tenant.IdIdentificationType,
                    IdentificationType = user.Tenant.IdentificationType?.Name,
                    IdentificationNumber = user.Tenant.IdentificationNumber
                };
            }

            return response;
        }
    }
}
