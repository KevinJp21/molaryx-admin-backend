using Application.Common.Mediator.Interfaces;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.LogOut;
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
                    "Token de refresco obtenido de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                ));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> LogOut([FromBody] LogOutCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                 new ApiResponse<bool>(
                     "Sesión cerrada de manera exitosa.",
                     await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}