using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Features.Product.Queries.GetAllProducts;
using Catalog.Application.Features.Product.Queries.GetProductById;
using Catalog.Application.Features.Product.Queries.GetProductByName;
using Catalog.Application.Features.Product.Queries.GetProductsByType;
using Catalog.Application.Features.Product.Commands.UpdateProduct;
using Catalog.Application.Features.Product.Commands.DeleteProduct;
using Catalog.Application.Common.Models;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    public class CatalogController : APIBaseController
    {
        private readonly IMediator mediator;
        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            BaseResponse response = await mediator.Send(new GetAllProductsQuery());
            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await mediator.Send(new GetProductByIdQuery(id));
            if (response.IsSuccess) return Ok(response.Data);
            return NotFound(response.Message);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var response = await mediator.Send(new GetProductByNameQuery(name));
            if (response.IsSuccess) return Ok(response.Data);
            return BadRequest(response.Message);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            var response = await mediator.Send(new GetProductsByTypeQuery(type));
            if (response.IsSuccess) return Ok(response.Data);
            return BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateProductCommand command)
        {
            // ensure id consistency
            if (id != command.Id) return BadRequest("Id mismatch");

            var response = await mediator.Send(command);
            if (response.IsSuccess) return Ok(response.Data);
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await mediator.Send(new DeleteProductCommand(id));
            if (response.IsSuccess) return NoContent();
            return BadRequest(response.Message);
        }
    }
}
