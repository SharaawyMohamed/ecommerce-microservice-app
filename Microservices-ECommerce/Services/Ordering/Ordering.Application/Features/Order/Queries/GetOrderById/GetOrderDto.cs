using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Queries.GetOrderById
{
	public record GetOrderDto
		(
		int Id,
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
		int PaymentMethod,
		string? CreatedBy,
		DateTime? CreatedDate,
		string? LastModifiedBy,
		DateTime? LastModifiedDate
		);
	
}
