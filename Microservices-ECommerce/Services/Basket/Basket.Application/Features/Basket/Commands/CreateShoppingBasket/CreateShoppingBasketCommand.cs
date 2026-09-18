using Catalog.Application.Common.Models;
using MediatR;
using Basket.Core.Entities;

namespace Basket.Application.Features.Basket.Commands.CreateShoppingBasket
{
	public record CreateShoppingBasketCommand(ShoppingCart Cart) : IRequest<BaseResponse>;

}
