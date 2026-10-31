using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Queries.GetDiscount
{
	public class GetDiscountByProductNameQueryValidator : AbstractValidator<GetDiscountByProductNameQuery>
	{
		public GetDiscountByProductNameQueryValidator()
		{
			RuleFor(x => x.productName).NotEmpty().WithMessage("Product Name is required");
		}
	}
}
