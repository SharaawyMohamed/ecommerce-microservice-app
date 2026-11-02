using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.Order.Queries.GetOrderById;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ordering.Application.Features.Order.Queries.GetOrdersByUserName
{
	internal class GetOrdersByUserNameQueryHandler : IRequestHandler<GetOrdersByUserNameQuery, BaseResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<GetOrdersByUserNameQueryHandler> _logger;
		public GetOrdersByUserNameQueryHandler(IOrderRepository orderRepository, ILogger<GetOrdersByUserNameQueryHandler> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
		{

			var orders = await _orderRepository.GetOrdersByUserNameAsync(request.userName);
			if(orders is null)
			{
				_logger.LogWarning("No orders found for user {UserName}", request.userName);
				return BaseResponse.Failure("Orders not found");
			}

			_logger.LogInformation("Orders retrieved for user {UserName}: {NumberOfOrders} orders found", request.userName, orders.Count());
			return BaseResponse.Success(orders.Adapt<List<GetOrderDto>>());

		}
	}
}
