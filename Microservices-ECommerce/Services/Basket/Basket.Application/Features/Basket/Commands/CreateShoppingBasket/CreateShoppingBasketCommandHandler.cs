using Basket.Application.Common.Modules;
using Basket.Application.Features.Basket.Queries.GetShoppingBasket;
using Basket.Application.GrpcServices;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Commands.CreateShoppingBasket
{
	public class CreateShoppingBasketCommandHandler : IRequestHandler<CreateShoppingCartBasketCommand, BaseResponse>
	{
		private readonly IBasketRepository _repository;
		private readonly ILogger<CreateShoppingBasketCommandHandler> _logger;
		public CreateShoppingBasketCommandHandler(IBasketRepository repository, ILogger<CreateShoppingBasketCommandHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(CreateShoppingCartBasketCommand request, CancellationToken cancellationToken)
		{

			var cart = request.Adapt<ShoppingCart>();
			cart.TotalPrice = cart.CalculateTotalPrice();

			await _repository.AddOrUpdateBasketAsync(cart);
			var basket = await _repository.GetBasketByUserNameAsync(request.UserName);
			_logger.LogInformation("Shopping basket created for user {UserName} with {ItemCount} items and total price {TotalPrice}", request.UserName, basket.Items.Count, basket.TotalPrice);
			return BaseResponse.Success(basket, "Basket saved successfully");
		}
	}
}
