using Application.Common.Mediator.Interfaces;
using Application.Features.Dashboard.Query.GetAppointmentsSummary;
using Application.Features.Dashboard.Query.GetPaymentsSummary;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class DashboardController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Policy = PermissionCodes.GET_PAYMENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<GetPaymentsSummaryResponse>>> GetPaymentsSummary(
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<GetPaymentsSummaryResponse>(
                    "Resumen de pagos obtenido de manera exitosa.",
                    await _mediator.Send(new GetPaymentsSummaryQuery(), cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.GET_APPOINTMENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<GetAppointmentsSummaryResponse>>> GetAppointmentsSummary(
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<GetAppointmentsSummaryResponse>(
                    "Resumen de citas obtenido de manera exitosa.",
                    await _mediator.Send(new GetAppointmentsSummaryQuery(), cancellationToken)
                )
            );
        }
    }
}
