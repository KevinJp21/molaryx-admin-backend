using Application.Common.Mediator.Interfaces;
using Application.DTOs;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Query.GetUser;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ApiResponse<LoginDTO>>> Get([FromBody] LoginCommand body, CancellationToken cancellationToken)
        {
            return (
                new ApiResponse<LoginDTO>(
                    "Inicio de sesion de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}