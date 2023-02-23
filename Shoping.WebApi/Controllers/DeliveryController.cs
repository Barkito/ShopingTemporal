using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.Categories.Commands;
using Shoping.Application.Features.Deliveries.Commands;
using Shoping.Application.Features.Deliveries.Queries;
using static Shoping.Application.Features.Deliveries.Queries.GetDeliveriesQueryHandler;

namespace Shoping.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetDeliveriesQueryResponse>> GetDelivery() => _mediator.Send(new GetDeliveriesQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        
        public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPut]
        [Authorize (Roles = "Admin")]

        public async Task<IActionResult> UpdateDelivery([FromBody] UpdateDeliveryCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("Id")]

        public Task<GetDeliveryQueryResponse> GetDeliveryById([FromRoute] GetDeliveryQuery query) =>
            _mediator.Send(query);

        [HttpDelete]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> DeleteDelivery(string id)
        {
            await _mediator.Send(new DeleteDeliveryCommand { Id = id });
            return Ok();
        }
    }
}
