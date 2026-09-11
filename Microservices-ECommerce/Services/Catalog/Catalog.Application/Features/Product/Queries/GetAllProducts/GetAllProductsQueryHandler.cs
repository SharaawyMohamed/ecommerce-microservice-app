using Catalog.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Queries.GetAllProducts
{
    public record GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, BaseResponse>
    {
        Task<BaseResponse> IRequestHandler<GetAllProductsQuery, BaseResponse>.Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
