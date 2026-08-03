using Application.Common.Mediator.Interfaces;
using Application.DTOs.Masters;
using Application.Features.Masters.Query.IdentificationType;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class MastersCOntroller(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IdentificationTypeDto>>> GetIdentificationType(
            [FromQuery] GetIdentificationTypeQuery query,
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