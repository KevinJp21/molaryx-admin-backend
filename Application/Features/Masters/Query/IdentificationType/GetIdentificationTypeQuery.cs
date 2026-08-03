using Application.Common.Mediator.Interfaces;
using Application.DTOs.Masters;

namespace Application.Features.Masters.Query.IdentificationType
{
    public class GetIdentificationTypeQuery : IRequest<List<IdentificationTypeDto>> { }
}