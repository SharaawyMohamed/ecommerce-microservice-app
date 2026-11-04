using Asp.Versioning;
using Basket.API.Controllers.V1;
using Basket.API.Models;
using Basket.Application.Common.Modules;
using Basket.Application.Features.Basket.Queries.GetShoppingBasket;
using EventBus.Messages;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.V2
{
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class BasketController:APIBaseController
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


		[HttpPost("checkout")]
		public async Task<IActionResult> Checkout([FromBody] BasketChekoutDtoV2 command)
		{
			var basket = await _mediator.Send(new GetShoppingBasketQuery(command.UserName));

			if (!basket.IsSuccess || basket.Data is null)
			{
				return NotFound(BaseResponse.Failure($"Mr/s {command.UserName}, Your Basket is Empty!! "));
			}

			var eventMessage = command.Adapt<BasketCheckoutEventV2>();
			_logger.LogInformation("BasketCheckoutEventV2 is being published to RabbitMQ: {BasketCheckoutEventV2}", eventMessage);

			// TODO:: update the consumer to sutable with the new version of the event message and also update the consumer to handle the new version of the event message.
			//TODO: apply coupon code and calculate the total price after discount

			await _publishEndpoint.Publish(eventMessage);

			return Accepted();
		}

	}
}
