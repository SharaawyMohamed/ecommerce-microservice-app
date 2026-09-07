using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Catalog.Infrustructure.Contexts
{
	public static class Dependencyinjection
	{
		public static IServiceCollection AddInfrustructure(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddMongoContext(configuration);
			return services;
		}

		private static IServiceCollection AddMongoContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

			services.AddSingleton<IMongoClient>(sp =>
			{
				var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

				return new MongoClient(settings.ConnectionString);
			});

			services.AddSingleton<IMongoDatabase>(sp =>
			{
				var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

				var client = sp.GetRequiredService<IMongoClient>();

				return client.GetDatabase(settings.DatabaseName);
			});

			services.AddSingleton<MongoDbContext>();

			return services;
		}
		public static async Task UseDatabaseSeeding(this IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();
			var services = scope.ServiceProvider;

			try
			{
				var context = services.GetRequiredService<MongoDbContext>();

				await DbInitializer.Seeder(context);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Critical: Database Seeding Failed: {ex.Message}");
			}
		}



	}

}
