using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrustructure.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrustructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IMongoCollection<Product> productCollection;
		public ProductRepository(MongoDbContext context)
		{
			productCollection = context.GetCollection<Product>(nameof(Product));
		}
		public async Task DeleteProductById(string id)
		{
			productCollection.DeleteOne(id);
		}

		public async Task<IEnumerable<Product>> GetAllProductsAsync()
		{
			return await productCollection.Find(Builders<Product>.Filter.Empty).ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductsByBTypeAsync(string type)
		{
			return await productCollection.Find(x => x.Type.Name == type).ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetAllProductsByNameAsync(string name)
		{
			return await productCollection.Find(x => x.Name == name).ToListAsync();
		}

		public async Task<Product> GetProductByIdAsync(string id)
		{
			return await productCollection.Find(x => x.id == id).FirstOrDefaultAsync();
		}

		public async Task UpdateProductAsync(Product product)
		{
			await productCollection.ReplaceOneAsync(x => x.id == product.id, product);
		}
	}
}
