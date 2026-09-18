using FluentValidation;

namespace Basket.Application.Features.Basket.Commands.DeleteShoppingBasket
{
	public class DeleteShoppingBasketValidator : AbstractValidator<DeleteShoppingBasketCommand>
	{
		public DeleteShoppingBasketValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("userName is required");
		}
	}
}
