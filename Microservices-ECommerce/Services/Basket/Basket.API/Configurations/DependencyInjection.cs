using Asp.Versioning;

namespace Basket.API.Configurations;

public static class DependencyInjection
{
	public static IServiceCollection AddAPIRegistration(this IServiceCollection services)
	{
		services.AddControllers();

		services.AddApiVersioning(options =>
		{
			options.DefaultApiVersion = new ApiVersion(1, 0);
			options.AssumeDefaultVersionWhenUnspecified = false;
			options.ReportApiVersions = true;
			options.ApiVersionReader = new UrlSegmentApiVersionReader();
		})
		.AddMvc()
		.AddApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'VVV";
			options.SubstituteApiVersionInUrl = true;
		});

		return services;
	}
}