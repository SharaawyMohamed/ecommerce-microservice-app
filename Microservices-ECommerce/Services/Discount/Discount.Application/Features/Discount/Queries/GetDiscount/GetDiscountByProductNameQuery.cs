using Discount.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Queries.GetDiscount
{
	public record GetDiscountByProductNameQuery(string productName) : IRequest<BaseResponse>;

}