using Discount.Application.Common.Models;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount
{
	public class CreateCouponDiscountCommandHandler : IRequestHandler<CreateCouponDiscountCommand, Coupon?>
	{
		private readonly IDiscountRepository _discountRepository;
		private readonly ILogger<CreateCouponDiscountCommandHandler> _logger;
		public CreateCouponDiscountCommandHandler(IDiscountRepository discountRepository, ILogger<CreateCouponDiscountCommandHandler> logger)
		{
			_discountRepository = discountRepository;
			_logger = logger;
		}
		public async Task<Coupon?> Handle(CreateCouponDiscountCommand request, CancellationToken cancellationToken)
		{
			var discount = request.Adapt<Coupon>();
			var coupon = await _discountRepository.CreateDiscountAsync(discount);
			if (coupon is not null)
			{
				_logger.LogInformation("Discount created for product name '{ProductName}': {Amount} off", request.ProductName, request.Amount);
				return coupon;
			}
			else
			{
				_logger.LogError("Failed to create discount for product name '{ProductName}'", request.ProductName);
				throw new InvalidOperationException("Failed to create discount");
			}
		}
	}
}
