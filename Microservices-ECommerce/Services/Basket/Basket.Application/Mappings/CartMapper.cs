using Basket.Application.Features.Basket.Commands.BasketChekout;
using Basket.Application.Features.Basket.Commands.CreateShoppingBasket;
using Basket.Application.Features.Basket.Queries.GetShoppingBasket;
using Basket.Application.Features.Basket.Queries.GetShoppingItem;
using Basket.Core.Entities;
using EventBus.Messages;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Mappings
{
	public class CartMapper : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<ShoppingCart, GetShoppingBasketResponse>();
			config.NewConfig<ShoppingCartItem, ShoppingBasketItemResponse>();
			config.NewConfig<BasketChekoutCommand, BasketCheckoutEventV1>();
			config.NewConfig<ShoppingCart, CreateShoppingCartBasketCommand>().TwoWays();
			config.NewConfig<ShoppingCart, GetShoppingBasketResponse>().TwoWays();
		}
	}
}
