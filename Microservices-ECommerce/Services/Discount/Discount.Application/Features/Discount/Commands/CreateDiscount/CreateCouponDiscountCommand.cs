using Discount.Application.Common.Models;
using Discount.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount
{
	public record CreateCouponDiscountCommand(string ProductName, int Amount, string Description) : IRequest<Coupon>;
	
	
}
