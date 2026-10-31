using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.DeleteDiscount
{
	public class DeleteDiscountValidator : AbstractValidator<DeleteDiscountCommand>
	{
		public DeleteDiscountValidator()
		{
			RuleFor(x => x.productName).NotEmpty().WithMessage("Product Name is required");
		}
	}
}
