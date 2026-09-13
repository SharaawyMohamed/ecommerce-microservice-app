using Catalog.Application.Features.Product.Queries.GetAllProducts;
using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Application.Tests
{
    [TestClass]
    public class GetAllProductsQueryHandlerTests
    {
        private class InMemoryProductRepository : IProductRepository
        {
            private readonly List<Product> _items = new();
            public InMemoryProductRepository()
            {
                _items.Add(new Product { id = "1", Name = "Item1", Description = "d", Price = 10, ImageFile = "i", Summary = "s", Brand = new ProductBrand { Name = "B" }, Type = new ProductType { Name = "T" } });
            }

            public Task AddAsync(Catalog.Core.Entities.Product entity)
            {
                _items.Add(entity);
                return Task.CompletedTask;
            }

            public Task DeleteProductById(string id)
            {
                _items.RemoveAll(x => x.id == id);
                return Task.CompletedTask;
            }

            public Task<IEnumerable<Product>> GetAllProductsAsync()
            {
                return Task.FromResult<IEnumerable<Product>>(_items);
            }

            public Task<IEnumerable<Product>> GetAllProductsByNameAsync(string name)
            {
                return Task.FromResult<IEnumerable<Product>>(_items.FindAll(x => x.Name == name));
            }

            public Task<Product> GetProductByIdAsync(string id)
            {
                return Task.FromResult(_items.Find(x => x.id == id));
            }

            public Task<IEnumerable<Product>> GetProductsByBTypeAsync(string type)
            {
                return Task.FromResult<IEnumerable<Product>>(_items.FindAll(x => x.Type.Name == type));
            }

            public Task UpdateProductAsync(Product product)
            {
                var idx = _items.FindIndex(x => x.id == product.id);
                if (idx >= 0) _items[idx] = product;
                return Task.CompletedTask;
            }
        }

        [TestMethod]
        public async Task Handle_ReturnsProducts()
        {
            var repo = new InMemoryProductRepository();
            var handler = new GetAllProductsQueryHandler(repo);

            var result = await handler.Handle(new GetAllProductsQuery(), default);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
        }
    }
}
