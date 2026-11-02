using Catalog.Application.Common.Models;
using Catalog.Application.Features.Product.Queries.GetProductById;
using Catalog.Core.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Catalog.Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;
		public GetAllProductsQueryHandler(IProductRepository productRepository, ILogger<GetAllProductsQueryHandler> logger)
        {
            _productRepository = productRepository;
			_logger = logger;
		}

        public async Task<BaseResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();  
                _logger.LogInformation("Retrieved {ProductCount} products from the repository", products.Count());
				return BaseResponse.Success(products.Adapt<List<GetProducResponse>>());
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Database unavailable while retrieving products");
                return BaseResponse.Failure("Database unavailable: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving products");
                return BaseResponse.Failure("Unexpected error: " + ex.Message);
            }
        }
    }
}
