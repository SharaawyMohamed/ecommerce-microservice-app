using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrustructure.Contexts
{
	public static class DbInitializer
	{
		public static async Task Seeder(MongoDbContext context)
		{
			await SeedTable(context.GetCollection<ProductBrand>("Brands"), "brands.json");

			await SeedTable(context.GetCollection<ProductType>("Types"), "types.json");

			await SeedTable(context.GetCollection<Product>("Products"), "products.json");
		}

		private static async Task SeedTable<T>(IMongoCollection<T> collection, string fileName) where T : class
		{
			var hasData = await collection.Find(Builders<T>.Filter.Empty).AnyAsync();

			if (hasData)
				return;

			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DataCollections",fileName);

			if (!File.Exists(filePath))
				return;

			var json = await File.ReadAllTextAsync(filePath);

			var entities = JsonSerializer.Deserialize<List<T>>(json,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			if (entities is { Count: > 0 })
			{
				await collection.InsertManyAsync(entities);
			}
		}
	}
}