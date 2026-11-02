using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Models;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Commands.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, BaseResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<DeleteOrderCommandHandler> _logger;
		public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			var order = (await _orderRepository.GetOrdersByUserNameAsync(request.userName)).FirstOrDefault();
			if(order is null)
			{
				_logger.LogError("Order not found for user {UserName}", request.userName);
				return BaseResponse.Failure("Order not found");
			}

			await _orderRepository.DeleteAsync(order!)!;
			await _orderRepository.SaveChangesAsync(cancellationToken);
			_logger.LogInformation("Order deleted successfully for user {UserName}", request.userName);
			return BaseResponse.Success("Order deleted successfully"); 
		}
	}
}
