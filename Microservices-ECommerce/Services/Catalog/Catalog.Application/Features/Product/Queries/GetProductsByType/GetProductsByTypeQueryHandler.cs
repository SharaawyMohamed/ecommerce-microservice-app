using Catalog.Application.Common.Models;
using Catalog.Application.Features.Product.Queries.GetProductById;
using Catalog.Core.Repositories;
using DnsClient.Internal;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Queries.GetProductsByType
{
    public class GetProductsByTypeQueryHandler : IRequestHandler<GetProductsByTypeQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductsByTypeQueryHandler> _logger;
		public GetProductsByTypeQueryHandler(IProductRepository productRepository, ILogger<GetProductsByTypeQueryHandler> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(GetProductsByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetProductsByBTypeAsync(request.productType);
                if(products is null)
                    return BaseResponse.Failure("No products found with the given type");

                _logger.LogInformation("Retrieved {ProductCount} products with type: {ProductType}", products.Count(), request.productType);
				return BaseResponse.Success(products.Adapt<List<GetProducResponse>>());
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Database unavailable while retrieving products with type: {ProductType}", request.productType);
                return BaseResponse.Failure("Database unavailable: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving products with type: {ProductType}", request.productType);
				return BaseResponse.Failure("Unexpected error: " + ex.Message);
            }
        }
    }
}
