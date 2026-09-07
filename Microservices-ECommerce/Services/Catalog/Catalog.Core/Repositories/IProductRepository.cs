using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Repositories
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAllProductsAsync();
		Task<IEnumerable<Product>> GetAllProductsByNameAsync(string name);
		Task<IEnumerable<Product>> GetAllProductsByBTypeAsync(string type);
		Task<Product> GetProductByIdAsync(string id);
		Task UpdateProductAsync(Product product);
		Task DeleteProductById(string id);
	}
}
