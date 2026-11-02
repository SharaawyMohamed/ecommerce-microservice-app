
using Basket.Application.GrpcServices;
using Discount.Application.Protos;
using FluentValidation;
using Mapster;
using MassTransit;
using MediatR;
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

			services.AddMassTransit(configur =>
			{
				configur.UsingRabbitMq((ctx, cfg) =>
				{
					cfg.Host(config["EventBusSettings:HostAddress"]);
				});
			});

			services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			return services;
		}
	}
}
