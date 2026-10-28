using Discount.Application.Protos;
using Discount.Core.Entities;
using Grpc.Core;
using MediatR;
using Discount.Application.Features.Discount.Queries.GetDiscount; // أو الـ Namespace الصح للـ Query بتاعتك
using Discount.Application.Features.Discount.Commands.CreateDiscount;
using Discount.Application.Features.Discount.Commands.UpdateDiscount;
using Discount.Application.Features.Discount.Commands.DeleteDiscount; // أو الـ Namespace الصح للـ Command بتاعتك

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
			if (response is not Coupon discount)
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

		public override async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = new CreateCouponDiscountCommand(request.ProductName, request.Amount, request.Description);

			var response = await _mediator.Send(coupon);
			if (response is not Coupon discount)
			{
				throw new RpcException(new Status(StatusCode.Cancelled, $"Discount creation is canceled."));
			}

			return new CreateDiscountResponse
			{
				ProductName = discount.ProductName,
				Description = discount.Description,
				Amount = discount.Amount
			};
		}
		public override async Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var response = await _mediator.Send(new UpdateDiscountCommand(request.Id, request.ProductName, request.Description, request.Amount));
			if (response is null || response is not Coupon discount)
			{
				throw new RpcException(new Status(StatusCode.Cancelled, $"Discount update is canceled."));
			}
			return new UpdateDiscountResponse
			{
				IsUpdated = true
			};
		}
	    
		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var response = await _mediator.Send(new DeleteDiscountCommand(request.ProductName));
			if (!response)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount for product {request.ProductName} not found."));
			}
			return new DeleteDiscountResponse
			{
				IsDeleted = true
			};
		}

	}
}
