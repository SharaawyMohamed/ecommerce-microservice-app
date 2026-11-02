using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Commands.DeleteProduct
{
	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse>
	{
		private readonly IProductRepository _productRepository;
		private readonly ILogger<DeleteProductCommandHandler> _logger;

		public DeleteProductCommandHandler(IProductRepository productRepository, ILogger<DeleteProductCommandHandler> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var existing = await _productRepository.GetProductByIdAsync(request.Id);
				if (existing == null)
					return BaseResponse.Failure("Product not found");

				await _productRepository.DeleteProductById(request.Id);
				_logger.LogInformation("Product deleted for ProductId: {ProductId}", request.Id);

				return BaseResponse.Success(null, "Product deleted");
			}
			catch (MongoDB.Driver.MongoException ex)
			{
				_logger.LogError(ex, "Database unavailable while deleting product with ProductId: {ProductId}", request.Id);
				return BaseResponse.Failure("Database unavailable: " + ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while deleting product with ProductId: {ProductId}", request.Id);
				return BaseResponse.Failure("Unexpected error: " + ex.Message);
			}
		}
	}
}
