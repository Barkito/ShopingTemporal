using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.Categories.Commands;
using Shoping.Application.Features.Categories.Queries;
using Shoping.Application.Features.Caterories.Queries;
using static Shoping.Application.Features.Caterories.Queries.GetCategoriesQueryHandler;
using System.Data;
using static Shoping.Application.Features.Inventory.Queries.GetInventoriesQueryHandler;
using Shoping.Application.Features.Inventory.Queries;
using Shoping.Application.Features.Inventory.Commands;

namespace Shoping.WebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Consulta los Categoryos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<List<GetInventoriesQueryResponse>> GetInventorys() => _mediator.Send(new GetInventoriesQuery());

        /// <summary>
        /// Crea un Categoryo nuevo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInventory([FromBody] CreateInventoryCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        //     /* /// <summary>
        //     /// Actualiza un Categoryo
        //     /// </summary>
        //     /// <param name="command"></param>
        //     /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateInventory([FromBody] UpdateInventoryCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        //     /// <summary>
        //     /// Consulta un Categoryo por su ID
        //     /// </summary>
        //     /// <param name="query"></param>

        //     /// <returns></returns>
        [HttpGet("{InventaryId}")]
        public Task<GetInventoryQueryResponse> GetInventaryById([FromRoute] GetInventoryQuery query) =>
           _mediator.Send(query);

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInventory(string Id)
        {
            await _mediator.Send(new DeleteInventoryCommand { Id = Id });

            return Ok();
        }
    }
}
