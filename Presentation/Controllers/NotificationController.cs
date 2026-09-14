using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Notification.Query.GetNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class NotificationController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetNotificationsResponse>>>> GetNotifications(
            [FromQuery] GetNotificationsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(new ApiResponse<PagedResult<GetNotificationsResponse>>(
                "Notificaciones obtenidas de manera exitosa.",
                await _mediator.Send(query, cancellationToken)));
        }
    }
}
