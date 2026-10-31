using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Queries.GetOrderById
{
	public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
	{
		public GetOrderByIdQueryValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Order ID must be greater than 0");
		}
	}
}
