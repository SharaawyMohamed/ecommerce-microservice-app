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
			return Ok(await mediator.Send(new GetAllProductsQuery()));
		}

		[HttpGet("search")]
		public async Task<IActionResult> GetByName([FromQuery] string name)
		{
			return Ok(await mediator.Send(new GetProductByNameQuery(name)));
		}

		[HttpGet("type/{type}")]
		public async Task<IActionResult> GetByType(string type)
		{
			return Ok(await mediator.Send(new GetProductsByTypeQuery(type)));
		}

		[HttpGet("{id}", Name = "GetProductById")]
		public async Task<IActionResult> GetById(string id)
		{
			return Ok(await mediator.Send(new GetProductByIdQuery(id)));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(string id, [FromBody] UpdateProductCommand command)
		{
			if (id != command.Id) return BadRequest("Id mismatch");

			return Ok(await mediator.Send(command));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			return Ok(await mediator.Send(new DeleteProductCommand(id)));
		}
	}
}