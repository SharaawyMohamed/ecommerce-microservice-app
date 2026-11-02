using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
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
		private readonly ILogger<GetOrderByIdQueryHandler> _logger;
		public GetOrderByIdQueryHandler(IOrderRepository orderRepository, ILogger<GetOrderByIdQueryHandler> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			var order = _orderRepository.GetOrderByIdAsync(request.Id);
			if(order is null)
			{
				_logger.LogWarning("Order not found for OrderId: {OrderId}", request.Id);
				return BaseResponse.Failure("Order not found");
			}

			_logger.LogInformation("Order retrieved for OrderId: {OrderId}", request.Id);
			return BaseResponse.Success(order.Adapt<GetOrderDto>());
		}
	}
}
