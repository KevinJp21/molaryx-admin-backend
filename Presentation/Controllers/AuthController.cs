using Application.Common.Mediator.Interfaces;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.Logout;
using Application.Features.Auth.Command.LogoutAll;
using Application.Features.Auth.Command.RefreshToken;
using Application.Features.Auth.Query.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LoginCommandResponse>>> Login([FromBody] LoginCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<LoginCommandResponse>(
                    "Inicio de sesion de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse<GetUserResponse>>> GetUSer([FromQuery] GetUserQuery query, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<GetUserResponse>(
                    "Información de usuario obtenida de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<RefreshTokenCommandResponse>>> RefreshToken([FromBody] RefreshTokenCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<RefreshTokenCommandResponse>(
                    "Refresh token actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                ));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> Logout([FromBody] LogoutCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                 new ApiResponse<bool>(
                     "Sesión cerrada de manera exitosa.",
                     await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> LogoutAll(CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Todas las sesiones fueron cerradas correctamente.",
                    await _mediator.Send(
                        new LogoutAllCommand(),
                        cancellationToken
                    )
                )
            );
        }
    }
}