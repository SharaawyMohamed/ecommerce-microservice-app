using Discount.Application.Common.Models;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Queries.GetDiscount
{
	public class GetDiscountByProductNameQueryHandler : IRequestHandler<GetDiscountByProductNameQuery, BaseResponse>
	{
		private readonly IDiscountRepository _discountRepository;

		public GetDiscountByProductNameQueryHandler(IDiscountRepository discountRepository)
		{
			_discountRepository = discountRepository;
		}

		public async Task<BaseResponse> Handle(GetDiscountByProductNameQuery request, CancellationToken cancellationToken)
		{
			var discount = await _discountRepository.GetDiscountAsync(request.productName);
			if(discount == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name '{request.productName}' not found."));
			}

			return BaseResponse.Success(discount);
		}
	}
}
