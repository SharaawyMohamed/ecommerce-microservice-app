using Discount.Application.Common.Models;
using Discount.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Queries.GetDiscount
{
	public record GetDiscountByProductNameQuery(string productName) : IRequest<Coupon>;

}