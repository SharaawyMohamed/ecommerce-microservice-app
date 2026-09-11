using Catalog.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Commands.UpdateProduct
{
    public record UpdateProductCommand
        (
        string Id,
        string Name,
        string Description,
        decimal Price,
        string Summry,
        string Brand,
        string Type
        ) : IRequest<BaseResponse>;
}
