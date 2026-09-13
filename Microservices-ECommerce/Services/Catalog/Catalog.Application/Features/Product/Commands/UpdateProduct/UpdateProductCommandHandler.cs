using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
                existing.Summary = request.Summry;
                existing.Brand = new ProductBrand { Name = request.Brand };
                existing.Type = new ProductType { Name = request.Type };

                await _productRepository.UpdateProductAsync(existing);

                return BaseResponse.Success(existing, "Product updated");
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
