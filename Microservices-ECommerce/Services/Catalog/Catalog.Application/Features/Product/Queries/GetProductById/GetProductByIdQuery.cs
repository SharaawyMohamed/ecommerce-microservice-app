using Catalog.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Queries.GetProductById
{
    public record GetProductByIdQuery(string Id):IRequest<BaseResponse>;
}
