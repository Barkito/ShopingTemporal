using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.Categories.Commands;
using Shoping.Application.Features.Caterories.Queries;
using Shoping.Application.Features.Customers.Commands;
using Shoping.Application.Features.Customers.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Shoping.Application.Features.Customers.Queries.GetClientiesQueryHadler;

namespace Shoping.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetClientiesQueryResponse>> GetClientys() => _mediator.Send(new GetClientiesQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateClienty([FromBody] CreateClientCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateClienty([FromBody] UpdateClientCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
        [HttpGet("{ClientyId}")]
        public Task<GetClientyQueryResponse> GetClientyById([FromRoute] GetClientyQuery query) =>
            _mediator.Send(query);

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClienty(string Id)
        {
            await _mediator.Send(new DeleteClientCommand { Id = Id });
            return Ok();
        }

    }
}
