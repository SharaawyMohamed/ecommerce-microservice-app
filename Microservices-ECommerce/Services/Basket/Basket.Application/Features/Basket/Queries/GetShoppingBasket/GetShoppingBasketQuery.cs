using Basket.Application.Common.Modules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Queries.GetShoppingBasket
{
	public record GetShoppingBasketQuery(string userName) : IRequest<BaseResponse>;
}
