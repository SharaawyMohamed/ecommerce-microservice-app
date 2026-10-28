using Discount.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.UpdateDiscount
{
	public record UpdateDiscountCommand(int Id, string ProductName, string Description, int Amount) : IRequest<Coupon>;
}
