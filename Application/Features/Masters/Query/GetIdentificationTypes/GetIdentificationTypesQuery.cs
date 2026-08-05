using Application.Common.Mediator.Interfaces;
using Application.DTOs.Masters;

namespace Application.Features.Masters.Query.GetIdentificationTypes
{
    public class GetIdentificationTypesQuery : IRequest<List<IdentificationTypeDto>> { }
}