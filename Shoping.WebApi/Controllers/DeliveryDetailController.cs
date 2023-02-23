using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.DeliveiesDetails.Commands;
using Shoping.Application.Features.DeliveiesDetails.Queries;
using static Shoping.Application.Features.DeliveiesDetails.Queries.GetDeliveriesDetailsQueryHandler;

namespace Shoping.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class DeliveryDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryDetailController(IMediator mediator)        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetDeliveriesDetailsQueryResponse>> GetDeliveriesDetails() => _mediator.Send(new GetDeliveriesDetailsQuery());

        [HttpPost]
        [Authorize (Roles = "Admin")]

        public async Task<IActionResult> GetDeliveryDetail([FromBody] CreateDeliveryDetailCommand  command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> UpdateDeliveryDetail([FromBody] UpdateDeliveryDetailCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{Id}")]

        public Task<GetDeliveryDetailQueryResponse> GetDeliveryDetailById([FromRoute] GetDeliveryDetailQuery query) =>
            _mediator.Send(query);

        [HttpDelete]
        [Authorize (Roles = "Admin")]

        public async Task<IActionResult> DeleteDeliryDetail(string Id)
        {
            await _mediator.Send(new DeleteDeliveryDetailCommand { Id = Id });
            return Ok();
        }
    }
    
}
