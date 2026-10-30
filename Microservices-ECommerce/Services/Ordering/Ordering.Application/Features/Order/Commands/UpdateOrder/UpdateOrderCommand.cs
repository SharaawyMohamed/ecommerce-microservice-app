using MediatR;
using Ordering.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Commands.UpdateOrder
{
	public record UpdateOrderCommand
	(
	    int Id,
	    string UserName,
	    string FirstName,
	    string LastName,
	    string EmailAddress,
	    string AddressLine,
	    string Country,
	    string State,
	    string ZipCode
		) : IRequest<BaseResponse>;
}
