using MediatR;
using MetroDFS.Services.Features.Stations.Commands.Add;
using MetroDFS.Services.Features.Stations.Commands.AddChild;
using MetroDFS.Services.Features.Stations.Queries.GetStations;
using MetroDFS.Services.Features.Trips.Queries.GetTripDirections;
using MetroDFS.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroDFS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MetroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("stations")]
        public async Task<ActionResult<Result<List<GetStationsQueryDto>>>> GetStations()
        {
            return Ok(await _mediator.Send(new GetStationsQuery()));
        }

        [HttpGet("trip")]
        public async Task<ActionResult<Result<GetTripDirectionsQueryDto>>> GetTripDirections([FromQuery] GetTripDirectionsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<Result<int>>> CreateStation(AddStationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("child")]
        public async Task<ActionResult<Result<string>>>CreateChild(AddChildCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
