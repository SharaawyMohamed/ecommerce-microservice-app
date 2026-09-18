using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Basket.Infrustructure.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDistributedCache _redis;

		public BasketRepository(IDistributedCache redis)
		{
			_redis = redis;
		}
		public async Task AddOrUpdateBasketAsync(ShoppingCart basket)
		{
			var cart = await _redis.GetStringAsync(basket.UserName);

			var serialized_obj = JsonSerializer.Serialize(basket);

			if (string.IsNullOrEmpty(cart))
			{
				var options = new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
				};

				await _redis.SetStringAsync(basket.UserName, serialized_obj, options);
			}
			else
			{
				await _redis.SetStringAsync(basket.UserName, serialized_obj);
			}

		}

		public async Task DeleteBasketAsync(string userName)
		{
			await _redis.RemoveAsync(userName);
		}

		public async Task<ShoppingCart> GetBasketAsync(string userName)
		{
			var cart = await _redis.GetStringAsync(userName);
			return (string.IsNullOrEmpty(cart) ? null
				: JsonSerializer.Deserialize<ShoppingCart>(cart))!;
		}

	}
}
