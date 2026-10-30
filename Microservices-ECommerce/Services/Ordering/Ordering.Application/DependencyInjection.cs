

using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Features.Order.Commands.CreateOrder;

namespace Ordering.Infrustructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

			return services;
		}
	}
}
