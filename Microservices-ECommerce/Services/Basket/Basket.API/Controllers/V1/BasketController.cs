using Asp.Versioning;
using Basket.API.Models;
using Basket.Application.Common.Modules;
using Basket.Application.Features.Basket.Commands.BasketChekout;
using Basket.Application.Features.Basket.Commands.CreateShoppingBasket;
using Basket.Application.Features.Basket.Commands.DeleteShoppingBasket;
using Basket.Application.Features.Basket.Queries.GetShoppingBasket;
using Basket.Core.Entities;
using EventBus.Messages;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Basket.API.Controllers.V1
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class BasketController : APIBaseController
	{
		private readonly IMediator _mediator;
		private readonly ILogger<BasketController> _logger;
		private readonly IPublishEndpoint _publishEndpoint;
		public BasketController(IMediator mediator, ILogger<BasketController> logger, IPublishEndpoint publishEndpoint)
		{
			_mediator = mediator;
			_logger = logger;
			_publishEndpoint = publishEndpoint;
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
		public async Task<IActionResult> Checkout([FromBody] BasketChekoutDtoV1 command)
		{
			var query = await _mediator.Send(new GetShoppingBasketQuery(command.UserName));

			if (!query.IsSuccess ||query.Data is null)
			{
				return NotFound(BaseResponse.Failure($"Mr/s {command.UserName}, Your Basket is Empty!! "));
			}

			var eventMessage = command.Adapt<BasketCheckoutEventV1>();
			_logger.LogInformation("BasketCheckoutEventV1 is being published to RabbitMQ: {BasketCheckoutEventV1}", eventMessage);
			
			//TODO: apply coupon code and calculate the total price after discount

			await _publishEndpoint.Publish(eventMessage);

			return Accepted();
		}
	}
}
