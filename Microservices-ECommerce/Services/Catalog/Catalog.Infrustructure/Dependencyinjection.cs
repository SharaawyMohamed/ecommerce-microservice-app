using Catalog.Core.Repositories;
using Catalog.Infrustructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
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
            // service registration

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IProductRepository, ProductRepository>();

            // Configure and register MongoDB context and related services in a single place
            services.AddMongoContext(configuration);

            return services;
        }

        private static IServiceCollection AddMongoContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.SectionName));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

                // Create MongoClient with short server selection timeout to fail fast when MongoDB is unreachable
                var mongoSettings = MongoDB.Driver.MongoClientSettings.FromConnectionString(settings.ConnectionString);
                mongoSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(3);
                mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(3);

                return new MongoClient(mongoSettings);
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
                var loggerFactory = services.GetService<Microsoft.Extensions.Logging.ILoggerFactory>();
                var logger = loggerFactory?.CreateLogger("DatabaseSeeding");
                if (logger != null)
                {
                    logger.LogError(ex, "Critical: Database Seeding Failed");
                }
                else
                {
                    System.Console.WriteLine($"Critical: Database Seeding Failed: {ex.Message}");
                }
            }
        }



    }

}
