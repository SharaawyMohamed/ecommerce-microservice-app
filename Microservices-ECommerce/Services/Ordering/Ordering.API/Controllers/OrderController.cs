using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.Order.Commands.CreateOrder;
using Ordering.Application.Features.Order.Commands.DeleteOrder;
using Ordering.Application.Features.Order.Commands.UpdateOrder;
using Ordering.Application.Features.Order.Queries.GetOrderById;
using Ordering.Application.Features.Order.Queries.GetOrdersByUserName;

namespace Ordering.API.Controllers
{
	public class OrderController : ApiBaseContoller
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<BaseResponse>> GetOrderById(int id)
		{
			return await _mediator.Send(new GetOrderByIdQuery(id));
		}

		[HttpGet]
		public async Task<ActionResult<BaseResponse>> GetUserOrders([FromQuery] string userName)
		{
			return await _mediator.Send(new GetOrdersByUserNameQuery(userName));
		}

		[HttpPut]
		public async Task<ActionResult<BaseResponse>> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
		{
			return await _mediator.Send(command);
		}

		[HttpPost]
		public async Task<ActionResult<BaseResponse>> CreateOrder([FromBody] CreateOrderCommand command)
		{
			return await _mediator.Send(command);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<BaseResponse>> DeleteOrder(int id)
		{
			return await _mediator.Send(new DeleteOrderCommand(id));
		}
	}
}
