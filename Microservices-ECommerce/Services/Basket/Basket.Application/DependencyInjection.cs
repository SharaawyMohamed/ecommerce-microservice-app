
using Basket.Application.GrpcServices;
using Discount.Application.Protos;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration config)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

			services.AddMapster();

			services.AddScoped<GrpcDiscountService>();
			services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
			{
				options.Address = new Uri(config["GrpcSettings:DiscountUrl"]!);
			});

			return services;
		}
	}
}
