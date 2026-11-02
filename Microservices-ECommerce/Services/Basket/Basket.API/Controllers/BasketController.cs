using Basket.API.Models;
using Basket.Application.Features.Basket.Commands.BasketChekout;
using Basket.Application.Features.Basket.Commands.CreateShoppingBasket;
using Basket.Application.Features.Basket.Commands.DeleteShoppingBasket;
using Basket.Application.Features.Basket.Queries.GetShoppingBasket;
using Basket.Core.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Basket.API.Controllers
{
	public class BasketController : APIBaseController
	{
		private readonly IMediator _mediator;

		public BasketController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{userName}")]
		public async Task<IActionResult> GetBasket(string userName)
		{
			var result = await _mediator.Send(new GetShoppingBasketQuery(userName));

			if (!result.IsSuccess)
				return NotFound(result.Message);

			return Ok(result.Data);
		}

		[HttpPost]
		public async Task<IActionResult> AddOrUpdateBasket([FromBody] CreateShoppingCartBasketCommand cart)
		{
			if (cart is null)
				return BadRequest("Invalid basket payload");

			return Ok(await _mediator.Send(cart));
		}

		[HttpDelete("{userName}")]
		public async Task<IActionResult> DeleteBasket(string userName)
		{
			return Ok(await _mediator.Send(new DeleteShoppingBasketCommand(userName)));
		}

		[HttpPost("checkout")]
		public async Task<IActionResult> Checkout([FromBody] BasketChekoutCommand command)
		{
			return Ok(await _mediator.Send(command));
		}
	}
}
