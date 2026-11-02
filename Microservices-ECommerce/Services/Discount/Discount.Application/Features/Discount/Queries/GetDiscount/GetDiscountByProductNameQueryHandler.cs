using Discount.Application.Common.Models;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Queries.GetDiscount
{
	public class GetDiscountByProductNameQueryHandler : IRequestHandler<GetDiscountByProductNameQuery, Coupon?>
	{
		private readonly IDiscountRepository _discountRepository;
		private readonly ILogger<GetDiscountByProductNameQueryHandler> _logger;
		public GetDiscountByProductNameQueryHandler(IDiscountRepository discountRepository, ILogger<GetDiscountByProductNameQueryHandler> logger)
		{
			_discountRepository = discountRepository;
			_logger = logger;
		}

		public async Task<Coupon?> Handle(GetDiscountByProductNameQuery request, CancellationToken cancellationToken)
		{
			var discount = await _discountRepository.GetDiscountAsync(request.productName);
			if(discount == null)
			{
				_logger.LogError("Discount with product name '{ProductName}' not found.", request.productName);
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name '{request.productName}' not found."));
			}

			_logger.LogInformation("Discount retrieved for product name '{ProductName}': {Amount} off", request.productName, discount.Amount);
			return discount;
		}
	}
}
