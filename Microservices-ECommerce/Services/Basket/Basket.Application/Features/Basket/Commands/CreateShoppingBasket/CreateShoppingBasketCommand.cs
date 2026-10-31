using MediatR;
using Basket.Core.Entities;
using Basket.Application.Common.Modules;

namespace Basket.Application.Features.Basket.Commands.CreateShoppingBasket
{
	public record CreateShoppingBasketCommand(ShoppingCart Cart) : IRequest<BaseResponse>;

}
