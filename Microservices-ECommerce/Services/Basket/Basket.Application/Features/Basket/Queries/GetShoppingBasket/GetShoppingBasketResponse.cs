using Basket.Application.Features.Basket.Queries.GetShoppingItem;
using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Queries.GetShoppingBasket
{
	public class GetShoppingBasketResponse
	{
		public string UserName { get; set; }
		public List<ShoppingBasketItemResponse> Items { get; set; } = new List<ShoppingBasketItemResponse>();

		public GetShoppingBasketResponse()
		{

		}
		public GetShoppingBasketResponse(string userName)
		{
			UserName = userName;
		}
	}
}
