using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Queries.GetProductsByType
{
    public class GetProductsByTypeQueryHandler : IRequestHandler<GetProductsByTypeQuery, BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsByTypeQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<BaseResponse> Handle(GetProductsByTypeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
