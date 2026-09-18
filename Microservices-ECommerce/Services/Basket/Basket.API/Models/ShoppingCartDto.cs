using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.API.Models
{
	public class ShoppingCartDto
	{
		public string UserName { get; set; }
		public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
	}
}
