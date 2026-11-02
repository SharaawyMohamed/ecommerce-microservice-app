using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Queries.GetProductById
{
    public record GetProducResponse(
        string Id,
        string Name,
        string Description,
        decimal Price,
        string ImageFile,
        string Summry,
        string Brand,
        string Type
        );
}
