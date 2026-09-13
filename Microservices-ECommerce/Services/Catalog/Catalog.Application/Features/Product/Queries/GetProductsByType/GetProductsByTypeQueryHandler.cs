using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Queries.GetProductsByType
{
    public class GetProductsByTypeQueryHandler : IRequestHandler<GetProductsByTypeQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByTypeQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse> Handle(GetProductsByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetProductsByBTypeAsync(request.productType);
                return BaseResponse.Success(products);
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
