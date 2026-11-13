using Catalog.Application.Features.Product.Queries.GetAllProducts;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Product.Queries.GetProductById;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Application.Tests
{
    public class GetAllProductsQueryHandlerTests
    {
        private class InMemoryProductRepository : IProductRepository
        {
            private readonly List<Product> _items = new();
            private readonly Exception? _getAllProductsException;

            public InMemoryProductRepository(Exception? getAllProductsException = null)
            {
                _getAllProductsException = getAllProductsException;
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
                if (_getAllProductsException is not null)
                {
                    throw _getAllProductsException;
                }

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

        [Fact]
        public async Task Handle_WhenRepositoryReturnsProducts_ReturnsMappedProducts()
        {
            var repo = new InMemoryProductRepository();
            var handler = new GetAllProductsQueryHandler(repo, NullLogger<GetAllProductsQueryHandler>.Instance);

            var result = await handler.Handle(new GetAllProductsQuery(), default);

            Assert.True(result.IsSuccess);
            var products = Assert.IsType<List<GetProducResponse>>(result.Data);
            Assert.Equal(1, products.Count);
            Assert.Equal("1", products[0].Id);
            Assert.Equal("Item1", products[0].Name);
        }

        [Fact]
        public async Task Handle_WhenRepositoryThrows_ReturnsFailureResponse()
        {
            var repo = new InMemoryProductRepository(new InvalidOperationException("repository failed"));
            var handler = new GetAllProductsQueryHandler(repo, NullLogger<GetAllProductsQueryHandler>.Instance);

            var result = await handler.Handle(new GetAllProductsQuery(), default);

            Assert.False(result.IsSuccess);
            Assert.Equal("Unexpected error: repository failed", result.Message);
        }
    }
}
