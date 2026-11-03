using EventBus.Messages;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.Order.Commands.CreateOrder;

namespace Ordering.Application.Common.Consumers
{
	public class BasketCheckoutEventConsumer : IConsumer<BasketCheckoutEventV1>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<BasketCheckoutEventConsumer> _logger;

		public BasketCheckoutEventConsumer(IMediator mediator, ILogger<BasketCheckoutEventConsumer> logger)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Consume(ConsumeContext<BasketCheckoutEventV1> context)
		{
			_logger.LogInformation("BasketCheckoutEventV1 received. CorrelationId: {CorrelationId}",context.Message.CorrelationId);

			var command = context.Message.Adapt<CreateOrderCommand>();
			_logger.LogInformation("Sending CreateOrderCommand for user: {UserName}",command.UserName);

			var result = await _mediator.Send(command);
			_logger.LogInformation("CreateOrderCommand completed. Result: {@Result}",result);
		}
	}
}
