using FluentValidation;

namespace Basket.Application.Features.Basket.Queries.GetShoppingBasket
{
	public class GetShoppingBasketValidator : AbstractValidator<GetShoppingBasketQuery>
	{
		public GetShoppingBasketValidator()
		{
			RuleFor(x => x.userName).NotEmpty().WithMessage("userName is required");
		}
	}
}
