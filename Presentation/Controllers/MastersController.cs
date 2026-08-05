using Application.Common.Mediator.Interfaces;
using Application.DTOs.Masters;
using Application.Features.Masters.Query.GetIdentificationTypes;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class MastersCOntroller(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<IdentificationTypeDto>>>> GetIdentificationTypes(
            [FromQuery] GetIdentificationTypesQuery query,
            CancellationToken cancellationToken
        )
        {
            return Ok(
                new ApiResponse<List<IdentificationTypeDto>>(
                    "Tipos de identificación obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}