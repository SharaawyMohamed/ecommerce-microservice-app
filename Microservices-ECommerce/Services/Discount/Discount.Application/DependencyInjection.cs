using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// Register MediatR handlers from this assembly
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

			// Register Mapster with DI - auto-discovers IRegister implementations
			services.AddMapster();

			services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			return services;
		}
	}
}
