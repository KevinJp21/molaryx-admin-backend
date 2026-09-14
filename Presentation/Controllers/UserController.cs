using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Users.Command.CreateMember;
using Application.Features.Users.Command.UpdateMember;
using Application.Features.Users.Query.GetProfile;
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
        [Authorize(Policy = PermissionCodes.GET_PROFILE)]
        [HttpGet]
        [EndpointDescription("Obtiene el perfil completo del usuario autenticado. Si es propietario, incluye los datos del consultorio.")]
        public async Task<ActionResult<ApiResponse<GetProfileQueryResponse>>> GetProfile(
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<GetProfileQueryResponse>(
                    "Perfil obtenido de manera exitosa.",
                    await _mediator.Send(new GetProfileQuery(), cancellationToken)
                )
            );
        }

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

        [Authorize(Policy = PermissionCodes.CREATE_MEMBER)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateMember(
            [FromBody] CreateMemberCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Miembro registrado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.UPDATE_MEMBER)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateMember(
            [FromBody] UpdateMemberCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Miembro actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
