using FluentValidation;
using MediatR;
using Ordering.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Commands.DeleteOrder
{
	public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
	{
		public DeleteOrderCommandValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Order ID must be greater than 0");
		}
	}

}
