using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice { get; set; } = 0;

        public decimal CalculateTotalPrice()
		{
			decimal totalPrice = 0;
			foreach (var item in Items)
			{
				totalPrice += item.Price * item.Quantity;
			}
			return totalPrice;
		}
	}
}
