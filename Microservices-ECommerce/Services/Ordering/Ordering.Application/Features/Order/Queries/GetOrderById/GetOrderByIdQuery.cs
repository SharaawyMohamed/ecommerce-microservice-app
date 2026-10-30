using MediatR;
using Ordering.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Queries.GetOrderById
{
	public record GetOrderByIdQuery(int Id) : IRequest<BaseResponse>;
}
