using Basket.Core.Entities;
using Basket.Core.Repositories;
using Catalog.Application.Common.Models;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Basket.Application.Features.Basket.Queries.GetShoppingBasket
{
	public class GetShoppingBasketQueryHandler : IRequestHandler<GetShoppingBasketQuery, BaseResponse>
	{
		private readonly IBasketRepository _basket;

		public GetShoppingBasketQueryHandler(IBasketRepository basket)
		{
			_basket = basket;
		}

		public async Task<BaseResponse> Handle(GetShoppingBasketQuery request, CancellationToken cancellationToken)
		{
			var basket = await _basket.GetBasketAsync(request.userName);

			if (basket is null)
			{

				return BaseResponse.Failure("The basket is not found!");
			}

			var maped_basket = basket.Adapt<GetShoppingBasketResponse>();
			return BaseResponse.Success(maped_basket);
		}
	}

}
