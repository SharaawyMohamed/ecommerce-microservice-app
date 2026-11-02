using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Core.Repositories;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Features.Order.Commands.CreateOrder
{
	public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand, BaseResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<CreateOrderCommandHandler> _logger;
		public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = request.Adapt<Core.Entities.Order>();
			await _orderRepository.AddAsync(order);
			await _orderRepository.SaveChangesAsync();
			_logger.LogInformation("Order created successfully");
			return BaseResponse.Success("Order created successfully");
		}
	}	
}
