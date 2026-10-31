using Discount.Core.Entities;
using Mapster;

namespace Discount.Application.Mappings
{
	public class CouponMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			// Domain Coupon entity mappings
			config.NewConfig<Coupon, Coupon>();
		}
	}
}
