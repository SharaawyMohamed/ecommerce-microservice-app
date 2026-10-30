using MediatR;
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

		public UpdateOrderCommandHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public Task<BaseResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = new Ordering.Core.Entityes.Order
			{
				UserName = request.UserName,
				FirstName = request.FirstName,
				LastName = request.LastName,
				EmailAddress = request.EmailAddress,
				AddressLine = request.AddressLine,
				Country = request.Country,
				State = request.State,
				ZipCode = request.ZipCode
			};
			_orderRepository.UpdateAsync(order);
			_orderRepository.SaveChangesAsync(cancellationToken);
			return Task.FromResult(BaseResponse.Success("Order updated successfully"));
		}
	}
}
