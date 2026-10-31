using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Commands.CreateOrder
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
			RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("TotalPrice must be greater than 0");
			RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
			RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress().WithMessage("Valid EmailAddress is required");
			RuleFor(x => x.AddressLine).NotEmpty().WithMessage("AddressLine is required");
			RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
			RuleFor(x => x.State).NotEmpty().WithMessage("State is required");
			RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required");
			RuleFor(x => x.CardName).NotEmpty().WithMessage("CardName is required");
			RuleFor(x => x.CardNumber).NotEmpty().CreditCard().WithMessage("Valid CardNumber is required");
			RuleFor(x => x.Expiration).NotEmpty().WithMessage("Expiration is required");
			RuleFor(x => x.CVV).NotEmpty().WithMessage("CVV is required");
			RuleFor(x => x.PaymentMethod).InclusiveBetween(1, 5).WithMessage("Valid PaymentMethod is required");
		}
	}

}
