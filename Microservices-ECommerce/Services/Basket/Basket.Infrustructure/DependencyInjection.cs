using Basket.Core.Repositories;
using Basket.Infrustructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrustructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrustructure(this IServiceCollection services, IConfiguration configuration)
		{
			// register repository
			services.AddScoped<IBasketRepository, BasketRepository>();

			// configure distributed cache (Redis)
			services.AddStackExchangeRedisCache(options =>
			{
				// try common configuration keys
				options.Configuration = configuration["CacheSettings:ConnectionString"]
										  ?? configuration.GetConnectionString("Redis");
				options.InstanceName = "Basket_";
			});

			return services;
		}
	}
}
