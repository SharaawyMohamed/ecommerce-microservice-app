

using FluentValidation;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Consumders;
using Ordering.Application.Common.Observers;
using Ordering.Application.Features.Order.Commands.CreateOrder;

namespace Ordering.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));
			services.AddSingleton<BusObserver>();
			services.AddMapster();

			services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddMassTransit(configur =>
			{
				configur.AddConsumer<BasketCheckoutEventConsumer>();

				configur.UsingRabbitMq((ctx, cfg) =>
				{
					cfg.Host(new Uri(config["EventBusSettings:HostAddress"]!));

					cfg.ConnectBusObserver(
						ctx.GetRequiredService<BusObserver>()
					);

					cfg.ReceiveEndpoint("basket-checkout-queue", c =>
					{
						c.ConfigureConsumer<BasketCheckoutEventConsumer>(ctx);
					});
				});
			});
			//TODO: sun of pitch line took 2 days for konw the problem!!
			services.AddMassTransitHostedService();

			return services;
		}
	}
}
