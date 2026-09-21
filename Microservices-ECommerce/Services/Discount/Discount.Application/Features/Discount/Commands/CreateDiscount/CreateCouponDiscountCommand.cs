using Discount.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount
{
	public record CreateCouponDiscountCommand(string ProductName, int Amount, string Description,int Id = 0) : IRequest<BaseResponse>;
	
	
}
