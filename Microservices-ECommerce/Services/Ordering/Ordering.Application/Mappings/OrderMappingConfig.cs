using Mapster;
using Ordering.Application.Features.Order.Commands.CreateOrder;
using Ordering.Application.Features.Order.Commands.UpdateOrder;
using Ordering.Application.Features.Order.Queries.GetOrderById;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappings
{
	public class OrderMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<CreateOrderCommand, Order>().TwoWays();
			config.NewConfig<Order, UpdateOrderCommand>().TwoWays();
			config.NewConfig<Order, GetOrderDto>().TwoWays();

		}

	}
}
