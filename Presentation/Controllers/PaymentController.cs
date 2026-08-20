using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Payment.Command.CreatePayment;
using Application.Features.Payment.Query.GetPaymentReport;
using Application.Features.Payment.Query.GetPayments;
using Application.Features.Payment.Query.GetPaymentsSummaryByConcept;
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
        [Authorize(Policy = PermissionCodes.GET_PAYMENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetPaymentsResponse>>>> GetPayments(
            [FromQuery] GetPaymentsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetPaymentsResponse>>(
                    "Pagos obtenidos de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.GET_PAYMENTS)]
        [HttpGet]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetPaymentReport(
            [FromQuery] GetPaymentReportQuery query,
            CancellationToken cancellationToken)
        {
            var (content, fileName) = await _mediator.Send(query, cancellationToken);

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        [Authorize(Policy = PermissionCodes.GET_PAYMENTS_SUMMARY_BY_CONCEPT)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<GetPaymentsSummaryByConceptResponse>>> GetPaymentsSummaryByConcept(
            [FromQuery] GetPaymentsSummaryByConceptQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<GetPaymentsSummaryByConceptResponse>(
                    "Resumen de pagos obtenido de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

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
