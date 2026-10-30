using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Queries.GetOrdersByUserName
{
	internal class GetOrdersByUserNameQueryHandler : IRequestHandler<GetOrdersByUserNameQuery, BaseResponse>
	{
		private readonly IOrderRepository _orderRepository;

		public GetOrdersByUserNameQueryHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<BaseResponse> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
		{
			var orders = await _orderRepository.GetOrdersByUserNameAsync(request.userName);
			return BaseResponse.Success(orders);
		}
	}
}
