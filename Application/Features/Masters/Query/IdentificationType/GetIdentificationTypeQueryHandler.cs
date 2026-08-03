using Application.Common.Mediator.Interfaces;
using Application.DTOs.Masters;
using Domain.Contracts;

namespace Application.Features.Masters.Query.IdentificationType
{
    public class GetIdentificationTypeQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetIdentificationTypeQuery, List<IdentificationTypeDto>>
    {
        public async Task<List<IdentificationTypeDto>> Handle(GetIdentificationTypeQuery request, CancellationToken cancellationToken = default)
        {
            var identificationTypes = await _unitOfWork.IdentificationTypeRepository.GetAll(cancellationToken) ??
                throw new InvalidOperationException("No se encontraron tipos de identificación");

            return [.. identificationTypes.Select(
                    identificationType => new IdentificationTypeDto
                    {
                        IdIdentificationType = identificationType.IdIdentificationType,
                        Code = identificationType.Code,
                        Name = identificationType.Name
                    }
                )];
        }
    }
}