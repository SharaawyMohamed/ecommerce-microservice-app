using FluentValidation;

namespace Basket.Application.Features.Basket.Commands.BasketChekout
{
	public class BasketCheckoutCommandValidator : AbstractValidator<BasketChekoutCommand>
	{
		public BasketCheckoutCommandValidator()
		{
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("UserName is required.")
				.NotNull()
				.MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");

			RuleFor(x => x.TotalPrice)
				.GreaterThan(0).WithMessage("TotalPrice must be greater than 0.");

			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("FirstName is required.")
				.MaximumLength(50);

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("LastName is required.")
				.MaximumLength(50);

			RuleFor(x => x.EmailAddress)
				.NotEmpty().WithMessage("EmailAddress is required.")
				.EmailAddress().WithMessage("EmailAddress must be a valid email format.");

			RuleFor(x => x.AddressLine)
				.NotEmpty().WithMessage("AddressLine is required.");

			RuleFor(x => x.Country)
				.NotEmpty().WithMessage("Country is required.");

			RuleFor(x => x.CardName)
				.NotEmpty().WithMessage("CardName is required.");

			RuleFor(x => x.CardNumber)
				.NotEmpty().WithMessage("CardNumber is required.");

			RuleFor(x => x.Expiration)
				.NotEmpty().WithMessage("Expiration date is required.");

			RuleFor(x => x.CVV)
				.NotEmpty().WithMessage("CVV is required.")
				.Length(3, 4).WithMessage("CVV must be 3 or 4 digits.");

			RuleFor(x => x.PaymentMethod)
				.InclusiveBetween(0, 2).WithMessage("PaymentMethod must be a valid option.");
		}
	}
}