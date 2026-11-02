using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

		public UpdateProductCommandHandler(IProductRepository productRepository, ILogger<UpdateProductCommandHandler> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _productRepository.GetProductByIdAsync(request.Id);
                if (existing == null)
                    return BaseResponse.Failure("Product not found");

                existing.Name = request.Name;
                existing.Description = request.Description;
                existing.Price = request.Price;
                existing.Summary = request.Summary;
                existing.Brand = new ProductBrand { Name = request.Brand };
                existing.Type = new ProductType { Name = request.Type };

                await _productRepository.UpdateProductAsync(existing);
                _logger.LogInformation("Product updated for ProductId: {ProductId}", request.Id);
                return BaseResponse.Success(existing, "Product updated");
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Database unavailable while updating product with ProductId: {ProductId}", request.Id);
				return BaseResponse.Failure("Database unavailable: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating product with ProductId: {ProductId}", request.Id);
				return BaseResponse.Failure("Unexpected error: " + ex.Message);
            }
        }
    }
}
