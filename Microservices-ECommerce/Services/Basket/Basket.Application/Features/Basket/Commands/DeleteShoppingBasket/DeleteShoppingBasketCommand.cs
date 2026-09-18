using Catalog.Application.Common.Models;
using MediatR;

namespace Basket.Application.Features.Basket.Commands.DeleteShoppingBasket
{
	public record DeleteShoppingBasketCommand(string UserName) : IRequest<BaseResponse>;
}
