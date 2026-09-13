using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(request.Id);
                if (product == null)
                    return BaseResponse.Failure("Product not found");

                return BaseResponse.Success(product);
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
