using Application.Common.Mediator.Interfaces;
using Application.Features.Plan.Query.GetPublicPlans;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PlanController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GetPublicPlansQueryResponse>>>> GetPublicPlans(
            [FromQuery] GetPublicPlansQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(new ApiResponse<List<GetPublicPlansQueryResponse>>(
                "Planes obtenidos de manera exitosa.",
                await _mediator.Send(query, cancellationToken)
            ));
        }
    }
}
