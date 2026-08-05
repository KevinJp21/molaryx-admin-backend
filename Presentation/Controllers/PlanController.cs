using Application.Common.Mediator.Interfaces;
using Application.Features.Plan.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PlanController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GetPlansQueryResponse>>>> GetPlans([FromQuery] GetPlansQuery query, CancellationToken cancellationToken)
        {
            return Ok(new ApiResponse<List<GetPlansQueryResponse>>(
                "Planes obtenidos de manera exitosa.",
                await _mediator.Send(query, cancellationToken)
            ));
        }
    }
}