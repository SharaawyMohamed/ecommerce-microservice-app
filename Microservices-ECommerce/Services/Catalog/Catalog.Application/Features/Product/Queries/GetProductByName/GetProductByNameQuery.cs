using Catalog.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Queries.GetProductByName
{
    public record GetProductByNameQuery(string Name) : IRequest<BaseResponse>;
}
