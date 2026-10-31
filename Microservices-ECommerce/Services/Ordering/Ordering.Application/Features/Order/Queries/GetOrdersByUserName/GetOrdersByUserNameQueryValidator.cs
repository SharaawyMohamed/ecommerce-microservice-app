using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Queries.GetOrdersByUserName
{
	public class GetOrdersByUserNameQueryValidator : AbstractValidator<GetOrdersByUserNameQuery>
	{
		public GetOrdersByUserNameQueryValidator()
		{
			RuleFor(x => x.userName).NotEmpty().WithMessage("UserName is required");
		}
	}
}
