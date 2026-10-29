
using Basket.Application.GrpcServices;
using Discount.Application.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration config)
		{
			// Register MediatR handlers from this assembly
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

			services.AddScoped<GrpcDiscountService>();
			services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
			{
				options.Address = new Uri(config["GrpcSettings:DiscountUrl"]!);
			});
			// Mapster/AutoMapper or other application registrations can go here

			return services;
		}
	}
}
