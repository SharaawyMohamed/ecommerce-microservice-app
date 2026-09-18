
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// Register MediatR handlers from this assembly
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

			// Mapster/AutoMapper or other application registrations can go here

			return services;
		}
	}
}
