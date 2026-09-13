using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Product.Queries.GetProductByName
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNameQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetAllProductsByNameAsync(request.Name);
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
