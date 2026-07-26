using Application.Common.Mediator.Interfaces;
using Application.DTOs.Auth;
using Application.DTOs.Sessions;
using Application.DTOs.Users;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.Logout;
using Application.Features.Auth.Command.LogoutAll;
using Application.Features.Auth.Command.RefreshToken;
using Application.Features.Auth.Query.GetSessions;
using Application.Features.Auth.Query.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpPost]
        [EndpointDescription( "Autentica al usuario y crea una nueva sesión, generando un access token y un refresh token." )]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] LoginCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<LoginResponseDto>(
                    "Inicio de sesion de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [HttpGet]
        [EndpointDescription( "Obtiene la información del usuario actualmente autenticado a partir de su sesión activa." )]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUser(CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<UserDto>(
                    "Información de usuario obtenida de manera exitosa.",
                    await _mediator.Send(new GetUserQuery(), cancellationToken)
                )
            );
        }

        [HttpGet]
        [EndpointDescription( "Obtiene las sesiones del usuario autenticado, incluyendo información del dispositivo, dirección IP, fecha de creación, fecha de expiración e identificación de la sesión actual." )]
        public async Task<ActionResult<ApiResponse<GetSessionsResponseDto>>> GetSessions([FromQuery] GetSessionsQuery query, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<GetSessionsResponseDto>(
                    "Sesiones obtenidas de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [AllowAnonymous]
        [HttpPost]
        [EndpointDescription( "Renueva el auth token y el refresh token utilizando un refresh token válido." )]
        public async Task<ActionResult<ApiResponse<RefreshTokenResponseDto>>> RefreshToken([FromBody] RefreshTokenCommand body, CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<RefreshTokenResponseDto>(
                    "Refresh token actualizado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                ));
        }

        [HttpPost]
        [EndpointDescription( "Cierra la sesión asociada al usuario autenticado y al refresh token proporcionado, invalidando dicho refresh token para evitar su reutilización." )]
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
        [EndpointDescription( "Cierra todas las sesiones activas del usuario autenticado, invalidando los refresh tokens asociados a sus sesiones." )]
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