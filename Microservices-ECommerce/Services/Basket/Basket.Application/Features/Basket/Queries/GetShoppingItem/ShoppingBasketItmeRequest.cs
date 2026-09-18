using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Queries.GetShoppingItem
{
	public class ShoppingBasketItmeRequest
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string ImageFile { get; set; }
	}
}
