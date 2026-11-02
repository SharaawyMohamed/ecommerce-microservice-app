using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Models;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Commands.UpdateOrder
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, BaseResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<UpdateOrderCommandHandler> _logger;
		public UpdateOrderCommandHandler(IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public Task<BaseResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = request.Adapt<Core.Entities.Order>();
			_orderRepository.UpdateAsync(order);
			_orderRepository.SaveChangesAsync(cancellationToken);
			_logger.LogInformation("Order updated successfully for user {UserName}", request.UserName);
			return Task.FromResult(BaseResponse.Success(order.Adapt<UpdateOrderCommand>(), "Order updated successfully"));
		}
	}
}
