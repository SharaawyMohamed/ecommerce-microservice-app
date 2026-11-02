using Discount.Application.Features.Discount.Commands.CreateDiscount;
using Discount.Core.Entities;
using Mapster;


namespace Discount.Application.Mappings
{
	public class CouponMapp : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Coupon,CreateCouponDiscountCommand>().TwoWays();
		}
	}
}
