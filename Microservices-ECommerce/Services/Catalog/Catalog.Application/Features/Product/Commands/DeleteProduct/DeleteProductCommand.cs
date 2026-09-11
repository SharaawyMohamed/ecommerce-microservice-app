using Catalog.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Features.Product.Commands.DeleteProduct
{
    public record DeleteProductCommand(string Id) : IRequest<BaseResponse>;
}
