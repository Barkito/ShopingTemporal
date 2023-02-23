using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.Sales.Commands;
using Shoping.Application.Features.Sales.Queries;
using static Shoping.Application.Features.Sales.Queries.GetSalesQueryHandler;

namespace Shoping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetSalesQueryResponse>> GetSales() => _mediator.Send(new GetSalesQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{SaleId}")]
        public Task<GetSaleQueryResponse> GetSaleById([FromRoute] GetSaleQuery query) =>
            _mediator.Send(query);

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSale(string Id)
        {
            await _mediator.Send(new DeleteSaleCommand { Id = Id });

            return Ok();
        }
    }
}