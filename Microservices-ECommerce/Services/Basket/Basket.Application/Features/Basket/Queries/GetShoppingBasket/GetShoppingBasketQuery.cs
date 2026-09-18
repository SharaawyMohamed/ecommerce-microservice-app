using Catalog.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Queries.GetShoppingBasket
{
	public record GetShoppingBasketQuery(string userName) : IRequest<BaseResponse>;
}
