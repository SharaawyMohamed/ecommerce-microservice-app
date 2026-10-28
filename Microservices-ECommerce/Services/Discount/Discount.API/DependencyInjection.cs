
using Discount.API.Discount.Grpc.Services;

namespace Discount.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDiscountAPI(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddGrpc();
			


			return services;
		}
	}
}
