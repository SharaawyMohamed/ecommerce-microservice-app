using MediatR;
using Ordering.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Commands.CreateOrder
{
	public record CreateOrderCommand(
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
