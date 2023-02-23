using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.Products.Commands;
using Shoping.Application.Features.Products.Queries;
using static Shoping.Application.Features.Products.Queries.GetProductsQueryHandler;
using GetProductQueryResponse = Shoping.Application.Features.Products.Queries.GetProductQueryResponse;

namespace Shoping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetProductsQueryResponse>> GetProducts() => _mediator.Send(new GetProductsQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{ProductId}")]
        public Task<GetProductQueryResponse> GetProductById([FromRoute] GetProductQuery query) => 
            _mediator.Send(query);

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = Id });

            return Ok();
        }
    }
}
