using Application.Common.Mediator.Interfaces;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Query.GetUser;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace WebAPI.Controllers
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
            var response = new ApiResponse<GetUserResponse>()
            {
                Ok = true,
                Message = "Informacion de usuario obtenida de manera exitosa.",
                Data = await _mediator.Send(query, cancellationToken)
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Get([FromBody] loginCommand body, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<LoginResponse>()
            {
                Ok = true,
                Message = "Inicio de sesion de manera exitosa.",
                Data = await _mediator.Send(body, cancellationToken)
            };

            return Ok(response);
        }
    }
}