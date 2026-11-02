using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

		public GetProductByIdQueryHandler(IProductRepository productRepository, ILogger<GetProductByIdQueryHandler> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(request.Id);
                if (product == null)
                {
                    _logger.LogWarning("Product not found for ProductId: {ProductId}", request.Id);
                    return BaseResponse.Failure("Product not found");
                }

                _logger.LogInformation("Product retrieved for ProductId: {ProductId}", request.Id);
				return BaseResponse.Success(product.Adapt<GetProducResponse>());
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Database unavailable while retrieving product with ProductId: {ProductId}", request.Id);
                return BaseResponse.Failure("Database unavailable: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving product with ProductId: {ProductId}", request.Id);
                return BaseResponse.Failure("Unexpected error: " + ex.Message);
            }
        }
    }
}
