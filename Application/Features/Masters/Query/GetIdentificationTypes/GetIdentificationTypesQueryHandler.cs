using Application.Common.Mediator.Interfaces;
using Application.DTOs.Masters;
using Domain.Contracts;

namespace Application.Features.Masters.Query.GetIdentificationTypes
{
    public class GetIdentificationTypesQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetIdentificationTypesQuery, List<IdentificationTypeDto>>
    {
        public async Task<List<IdentificationTypeDto>> Handle(GetIdentificationTypesQuery request, CancellationToken cancellationToken = default)
        {
            var identificationTypes = await _unitOfWork.IdentificationTypeRepository.GetAll(cancellationToken: cancellationToken) ??
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