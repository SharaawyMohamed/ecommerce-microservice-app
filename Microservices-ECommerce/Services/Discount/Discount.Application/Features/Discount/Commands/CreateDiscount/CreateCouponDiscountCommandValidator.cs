using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount
{
	public class CreateCouponDiscountCommandValidator : AbstractValidator<CreateCouponDiscountCommand>
	{
		public CreateCouponDiscountCommandValidator()
		{
			RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product Name is required");
			RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
		}
	}
}
