using Application.Common.Mediator.Interfaces;
using Application.Features.Payment.Command.CreatePayment;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PaymentController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.CREATE_PAYMENT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreatePayment(
            [FromBody] CreatePaymentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Pago registrado de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
