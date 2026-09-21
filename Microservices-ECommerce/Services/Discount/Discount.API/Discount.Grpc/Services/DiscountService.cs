using Discount.Application.Features.Discount.Commands.CreateDiscount;
using Discount.Application.Features.Discount.Queries.GetDiscount;
using Discount.Application.Protos;
using Discount.Core.Entities;
using Grpc.Core;
using MediatR;

namespace Discount.API.Discount.Grpc.Services
{
	public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
	{
		private readonly IMediator _mediator;

		public DiscountService(IMediator mediator)
		{
			_mediator = mediator;
		}

		public override async Task<GetDiscountResponse> GetDiscount(GetDiscountQuery request, ServerCallContext context)
		{
			var response = await _mediator.Send(new GetDiscountByProductNameQuery(request.ProductName));
			if (response.Data is not GetDiscountResponse discount)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount for product {request.ProductName} not found."));
			}

			return new GetDiscountResponse
			{
				Id = discount.Id,
				ProductName = discount.ProductName,
				Description = discount.Description,
				Amount = discount.Amount
			};

		}

		public override async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountCommand request, ServerCallContext context)
		{
			var coupon = new CreateCouponDiscountCommand(request.ProductName, request.Amount, request.Description);

			var response = await _mediator.Send(coupon);
			if (response.Data is not CreateCouponDiscountCommand discount)
			{
				throw new RpcException(new Status(StatusCode.Cancelled, $"Discount creation is canceled."));
			}

			return new CreateDiscountResponse
			{
				Id = discount.Id,
				ProductName = discount.ProductName,
				Description = discount.Description,
				Amount = discount.Amount
			};
		}
	}
}
