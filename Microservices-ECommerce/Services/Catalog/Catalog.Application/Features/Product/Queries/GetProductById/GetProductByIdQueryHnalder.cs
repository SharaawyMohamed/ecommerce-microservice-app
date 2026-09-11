using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHnalder : IRequestHandler<GetProductByIdQuery,BaseResponse>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHnalder(IProductRepository productRepository)
        {
                _productRepository = productRepository;
        }

        public Task<BaseResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
