using Ocelot.DependencyInjection;

namespace Ocelot.APIGatway
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddOcelotAPIGatwayConfigurations(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOcelot(configuration);
			return services;
		}
	}
}
