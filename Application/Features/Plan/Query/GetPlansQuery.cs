using Application.Common.Mediator.Interfaces;

namespace Application.Features.Plan.Query
{
    public class GetPlansQuery : IRequest<List<GetPlansQueryResponse>> {}
}