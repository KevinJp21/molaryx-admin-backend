using Application.Common.Mediator.Interfaces;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Query.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult<ApiResponse<LoginCommandResponse>>> Get([FromBody] LoginCommand body, CancellationToken cancellationToken)
        {
            return (
                new ApiResponse<LoginCommandResponse>(
                    "Inicio de sesion de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<ApiResponse<GetUserResponse>>> Get([FromQuery] GetUserQuery query, CancellationToken cancellationToken)
        {
            return (
                new ApiResponse<GetUserResponse>(
                    "Información de usuario obtenida de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }
    }
}