using Discount.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Core.Repositories
{
	public interface IDiscountRepository
	{
		Task<Coupon> GetDiscountAsync(string productName);
		Task<Coupon> CreateDiscountAsync(Coupon coupon);
		Task<bool> UpdateDiscountAsync(Coupon coupon);
		Task<bool> DeleteDiscountAsync(string productName);
	}
}
