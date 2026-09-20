
using Discount.Core.Repositories;
using Discount.Infrustructure.Presistence;
using Discount.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrustructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrustructure(this IServiceCollection services, IConfiguration configuration)
		{

			var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string missing.");

			services.AddDbContext<DiscountDbContext>(options =>
				options.UseNpgsql(connectionString));

			services.AddScoped<IDiscountRepository>(sp =>new DiscountRepository(connectionString));


			return services;
		}
	}
}
