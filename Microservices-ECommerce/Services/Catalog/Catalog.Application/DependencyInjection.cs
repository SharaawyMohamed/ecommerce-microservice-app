using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// Register MediatR handlers from this assembly
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

			// Register Mapster with DI - auto-discovers IRegister implementations
			services.AddMapster();

			return services;
		}
	}
}
