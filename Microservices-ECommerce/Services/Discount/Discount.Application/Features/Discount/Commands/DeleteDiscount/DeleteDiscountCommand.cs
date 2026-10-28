using Discount.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.DeleteDiscount
{
	public record DeleteDiscountCommand(string productName) : IRequest<bool>;
}
