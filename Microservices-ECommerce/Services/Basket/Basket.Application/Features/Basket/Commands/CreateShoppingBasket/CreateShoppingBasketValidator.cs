using FluentValidation;

namespace Basket.Application.Features.Basket.Commands.CreateShoppingBasket
{
	public class CreateShoppingBasketValidator : AbstractValidator<CreateShoppingBasketCommand>
	{
		public CreateShoppingBasketValidator()
		{
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart payload is required");
			RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
		}
	}
}
