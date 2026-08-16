using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Users.Query.GetProfessionals;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UserController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_PROFESSIONALS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetProfessionalsResponse>>>> GetProfessionals(
            [FromQuery] GetProfessionalsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetProfessionalsResponse>>(
                    "Profesionales obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}
