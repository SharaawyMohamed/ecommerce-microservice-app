using MediatR;
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

		public DeleteOrderCommandHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<BaseResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			var order = await _orderRepository.GetOrderByIdAsync(request.Id);
			await _orderRepository.DeleteAsync(order)!;
			await _orderRepository.SaveChangesAsync(cancellationToken);
			return BaseResponse.Success("Order deleted successfully"); 
		}
	}
}
