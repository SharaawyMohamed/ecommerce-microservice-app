using Basket.Application.Common.Modules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Commands.BasketChekout
{
	public record BasketChekoutCommand(
		 string UserName,
	     decimal TotalPrice,
	     string FirstName,
	     string LastName,
	     string EmailAddress,
	     string AddressLine,
	     string Country,
	     string State,
	     string ZipCode,
	     string CardName,
	     string CardNumber,
	     string Expiration,
	     string CVV,
	     int PaymentMethod
		) : IRequest<BaseResponse>;
}
