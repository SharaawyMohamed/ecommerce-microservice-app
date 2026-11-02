using Catalog.Application.Common.Models;
using Catalog.Application.Features.Product.Queries.GetProductById;
using Catalog.Core.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Queries.GetProductByName
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductByNameQueryHandler> _logger;
		public GetProductByNameQueryHandler(IProductRepository productRepository, ILogger<GetProductByNameQueryHandler> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetAllProductsByNameAsync(request.Name);
                if (products == null)
                {
                    return BaseResponse.Failure("No products found with the given name");
                }
                _logger.LogInformation("Retrieved {ProductCount} products with name: {ProductName}", products.Count(), request.Name);
				return BaseResponse.Success(products.Adapt<List<GetProducResponse>>());
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Database unavailable while retrieving products with name: {ProductName}", request.Name);
                return BaseResponse.Failure("Database unavailable: " + ex.Message);
            }
            catch (System.Exception ex)
            {
				_logger.LogError(ex, "Unexpected error occurred while retrieving products with name: {ProductName}", request.Name);
				return BaseResponse.Failure("Unexpected error: " + ex.Message);
            }
        }
    }
}
