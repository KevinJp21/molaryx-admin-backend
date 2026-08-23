using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Users.Query.GetTeam;
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
        [Authorize(Policy = PermissionCodes.GET_TEAM)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetTeamResponse>>>> GetTeam(
            [FromQuery] GetTeamQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetTeamResponse>>(
                    "Equipo obtenido de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}
