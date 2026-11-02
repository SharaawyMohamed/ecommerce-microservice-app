using Basket.Application.Common.Modules;
using Basket.Application.GrpcServices;
using Basket.Core.Repositories;
using EventBus.Messages;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Commands.BasketChekout
{
	public class BasketChekoutCommandHandler : IRequestHandler<BasketChekoutCommand, BaseResponse>
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly GrpcDiscountService _grpcDiscountService;
		private readonly IBasketRepository _basket;
		private readonly ILogger<BasketChekoutCommandHandler> _logger;

		public BasketChekoutCommandHandler(
			IBasketRepository basketRepository,
			IPublishEndpoint publishEndpoint,
			GrpcDiscountService grpcDiscountService,
			IBasketRepository basket,
			ILogger<BasketChekoutCommandHandler> logger)
		{
			_basketRepository = basketRepository;
			_publishEndpoint = publishEndpoint;
			_grpcDiscountService = grpcDiscountService;
			_basket = basket;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(BasketChekoutCommand request, CancellationToken cancellationToken)
		{
			var basket = await _basketRepository.GetBasketByUserNameAsync(request.UserName);
			if (basket is null || !basket.Items.Any())
			{
				return BaseResponse.Failure($"Mr/s {request.UserName}, Your Basket is Empty!! ");
			}

			var eventMessage = request.Adapt<BasketCheckoutEvent>();
			_logger.LogInformation("BasketCheckoutEvent is being published to RabbitMQ: {BasketCheckoutEvent}", eventMessage);
			//TODO: apply coupon code and calculate the total price after discount

			//var userBasket= await _basket.GetBasketByUserNameAsync(request.UserName);

			//decimal discount = 0;
			//foreach (var item in userBasket.Items)
			//{
			//	var coupon = await _grpcDiscountService.GetDiscount(item.ProductName);
			//	if (coupon is not null)
			//	{
			//		discount += coupon.Amount;
			//	}
			//}

			//userBasket.TotalPrice -= discount;	
			//await _basket.AddOrUpdateBasketAsync(userBasket);


			await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage, cancellationToken);

			return BaseResponse.Success();
		}
	}
}
