

using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Features.Order.Commands.CreateOrder;

namespace Ordering.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// Register MediatR handlers from this assembly
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

			// Register Mapster with DI - auto-discovers IRegister implementations
			services.AddMapster();

			return services;
		}
	}
}
