

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrustructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrustructure(this IServiceCollection services, IConfiguration configuration)
		{
			// register repository
			services.AddScoped<IOrderRepository, OrderRepository>();

			// configure distributed cache SQL
			services.AddDbContextFactory<OrderingDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
			});

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(OrderRepository).Assembly));

			return services;
		}
	}
}
