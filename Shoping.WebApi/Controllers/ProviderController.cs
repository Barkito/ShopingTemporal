using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Shoping.Application.Features.Providers.Queries.GetProvidersQueryHandler;
using Shoping.Application.Features.Providers.Queries;
using Shoping.Application.Features.Providers.Commands;

namespace Shoping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetProvidersQueryResponse>> GetProviders() => _mediator.Send(new GetProvidersQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProvider([FromBody] CreateProviderCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProvider([FromBody] UpdateProviderCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{ProviderId}")]
        public Task<GetProviderQueryResponse> GetProviderById([FromRoute] GetProviderQuery query) =>
            _mediator.Send(query);

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProvider(string Id)
        {
            await _mediator.Send(new DeleteProviderCommand { Id = Id });

            return Ok();
        }
    }
}
