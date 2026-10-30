using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Queries.GetOrderById
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, BaseResponse>
	{
		private readonly IOrderRepository _orderRepository;

		public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<BaseResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			var order = _orderRepository.GetOrderByIdAsync(request.Id);
			return order == null ? BaseResponse.Failure("Order not found") : BaseResponse.Success(order);
		}
	}
}
