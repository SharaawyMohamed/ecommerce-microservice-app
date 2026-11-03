using Basket.API.Models;
using EventBus.Messages;
using Mapster;

namespace Basket.API.Mappings
{
	public class ChekoutMappers : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<BasketChekoutDtoV2, BasketCheckoutEventV2>().TwoWays();
			config.NewConfig<BasketChekoutDtoV1, BasketCheckoutEventV1>().TwoWays();
		}
	}
}
