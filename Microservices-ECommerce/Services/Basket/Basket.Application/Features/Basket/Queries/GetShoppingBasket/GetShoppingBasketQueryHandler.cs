using Basket.Application.Common.Modules;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Basket.Application.Features.Basket.Queries.GetShoppingBasket
{
	public class GetShoppingBasketQueryHandler : IRequestHandler<GetShoppingBasketQuery, BaseResponse>
	{
		private readonly ILogger<GetShoppingBasketQueryHandler> _logger;
		private readonly IBasketRepository _basket;

		public GetShoppingBasketQueryHandler(IBasketRepository basket, ILogger<GetShoppingBasketQueryHandler> logger)
		{
			_basket = basket;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(GetShoppingBasketQuery request, CancellationToken cancellationToken)
		{
			var basket = await _basket.GetBasketByUserNameAsync(request.userName);

			if (basket is null)
			{

				return BaseResponse.Failure("The basket is not found!");
			}

			var maped_basket = basket.Adapt<GetShoppingBasketResponse>();
			_logger.LogInformation("Shopping basket retrieved for user {UserName}: {Number of Basket Items}:", request.userName, maped_basket.Items.Count());
			return BaseResponse.Success(maped_basket);
		}
	}

}
