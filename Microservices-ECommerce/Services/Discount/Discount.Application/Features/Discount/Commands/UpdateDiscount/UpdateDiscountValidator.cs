using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.UpdateDiscount
{
	public class UpdateDiscountValidator : AbstractValidator<UpdateDiscountCommand>
	{
		public UpdateDiscountValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Discount ID must be greater than 0");
			RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product Name is required");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
			RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
		}
	}
}
