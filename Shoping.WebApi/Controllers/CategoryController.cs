using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.Categories.Commands;
using Shoping.Application.Features.Categories.Queries;
using Shoping.Application.Features.Caterories.Queries;
using static Shoping.Application.Features.Caterories.Queries.GetCategoriesQueryHandler;

namespace Shoping.WebApi.Controllers
{
     [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Consulta los Categoryos
        /// </summary>
        /// <returns></returns>
        [HttpGet]   
        public Task<List<GetCategoriesQueryResponse>> GetCategorys() => _mediator.Send(new GetCategoriesQuery());

        /// <summary>
        /// Crea un Categoryo nuevo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
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
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        //     /// <summary>
        //     /// Consulta un Categoryo por su ID
        //     /// </summary>
        //     /// <param name="query"></param>
        //     /// <returns></returns>
        [HttpGet("{CategoryId}")]
        public Task<GetCategoryQueryResponse> GetCategoryById([FromRoute] GetCategoryQuery query) =>
           _mediator.Send(query);
    


       [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory( string Id)
        {
            await _mediator.Send(  new DeleteCategoryCommand{Id=Id});

            return Ok();
        }
}

}