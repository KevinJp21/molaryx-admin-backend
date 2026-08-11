using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Users.Patient.Query;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers.User
{
    [Authorize]
    [Route("api/v1/user/patients/[action]")]
    [ApiController]
    public class UserPatientsController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_USER_PATIENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetPatientsResponse>>>> GetPatients(
            [FromQuery] GetPatientsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetPatientsResponse>>(
                    "Pacientes obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}