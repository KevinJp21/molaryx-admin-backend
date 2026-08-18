using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Features.Appointment.Command.CreateAppointment;
using Application.Features.Appointment.Command.UpdateAppointment;
using Application.Features.Appointment.Query.GetAppointments;
using Application.Features.Appointment.Query.GetAppointmentsList;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AppointmentController(IMediator _mediator) : ControllerBase
    {

        [Authorize(Policy = PermissionCodes.GET_APPOINTMENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<GetAppointmentsResponse[]>>> GetAppointments(
            [FromQuery] GetAppointmentsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<GetAppointmentsResponse[]>(
                    "Citas obtenidas de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.GET_APPOINTMENTS)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<GetAppointmentsListResponse>>>> GetAppointmentsList(
            [FromQuery] GetAppointmentsListQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<PagedResult<GetAppointmentsListResponse>>(
                    "Citas obtenidas de manera exitosa.",
                    await _mediator.Send(query, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.CREATE_APPOINTMENT)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateAppointment(
            [FromBody] CreateAppointmentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Cita creada de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }

        [Authorize(Policy = PermissionCodes.UPDATE_APPOINTMENT)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateAppointment(
            [FromBody] UpdateAppointmentCommand body,
            CancellationToken cancellationToken)
        {
            return Ok(
                new ApiResponse<bool>(
                    "Cita actualizada de manera exitosa.",
                    await _mediator.Send(body, cancellationToken)
                )
            );
        }
    }
}
