using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketByUserNameAsync(string userName);

		//TODO: Add Or Update & Delete:  Basket method should return the updated basket instead of void to reflect the changes made to the basket.
		Task AddOrUpdateBasketAsync(ShoppingCart basket);
        Task DeleteBasketAsync(string userName);
    }
}
