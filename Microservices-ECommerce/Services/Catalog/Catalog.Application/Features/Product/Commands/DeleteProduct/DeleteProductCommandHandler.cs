using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _productRepository.GetProductByIdAsync(request.Id);
                if (existing == null)
                    return BaseResponse.Failure("Product not found");

                await _productRepository.DeleteProductById(request.Id);

                return BaseResponse.Success(null, "Product deleted");
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                return BaseResponse.Failure("Database unavailable: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                return BaseResponse.Failure("Unexpected error: " + ex.Message);
            }
        }
    }
}
