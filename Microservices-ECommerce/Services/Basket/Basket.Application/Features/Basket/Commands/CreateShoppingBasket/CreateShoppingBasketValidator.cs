using FluentValidation;

namespace Basket.Application.Features.Basket.Commands.CreateShoppingBasket
{
	public class CreateShoppingBasketValidator : AbstractValidator<CreateShoppingCartBasketCommand>
	{
		public CreateShoppingBasketValidator()
		{
			RuleFor(x => x.Items).NotNull().WithMessage("Cart payload is required");
			RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
		}
	}
}