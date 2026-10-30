using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Order.Commands.CreateOrder
{
	public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand, BaseResponse>
	{
		private readonly IOrderRepository _orderRepository;

		public CreateOrderCommandHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<BaseResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = new Core.Entityes.Order
			{
				UserName = request.UserName,
				TotalPrice = request.TotalPrice,
				FirstName = request.FirstName,
				LastName = request.LastName,
				EmailAddress = request.EmailAddress,
				AddressLine = request.AddressLine,
				Country = request.Country,
				State = request.State,
				ZipCode = request.ZipCode,
				CardName = request.CardName,
				CardNumber = request.CardNumber,
				Expiration = request.Expiration,
				CVV = request.CVV,
				PaymentMethod = request.PaymentMethod
			};
			await _orderRepository.AddAsync(order);
			await _orderRepository.SaveChangesAsync();
			return BaseResponse.Success("Order created successfully");
		}
	}	
}
