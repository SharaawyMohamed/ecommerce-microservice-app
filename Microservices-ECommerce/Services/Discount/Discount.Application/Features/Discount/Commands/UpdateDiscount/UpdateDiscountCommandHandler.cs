using Discount.Application.Protos;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.UpdateDiscount
{
	public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, Coupon>
	{
		private readonly IDiscountRepository _discountRepository;
		private readonly ILogger<UpdateDiscountCommandHandler> _logger;
		public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, ILogger<UpdateDiscountCommandHandler> logger)
		{
			_discountRepository = discountRepository;
			_logger = logger;
		}

		public async Task<Coupon> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
		{
			var coupon = await _discountRepository.GetDiscountAsync(request.ProductName);
			if (coupon is null || coupon.Id != request.Id)
			{
				_logger.LogError("Discount with product name '{ProductName}' not found or Id mismatch.", request.ProductName);
				throw new InvalidOperationException($"Discount with product name '{request.ProductName}' not found or Id mismatch.");
			}

			var updatedCoupon = new Coupon
			{
				ProductName = request.ProductName,
				Description = request.Description,
				Amount = request.Amount
			};

			var is_updated = await _discountRepository.UpdateDiscountAsync(updatedCoupon);
			if (is_updated)
			{
				_logger.LogInformation("Discount updated for product name '{ProductName}': {Amount} off", request.ProductName, request.Amount);
				return updatedCoupon;
			}
			else
			{
				_logger.LogError("Failed to update discount for product name '{ProductName}'", request.ProductName);
				throw new InvalidOperationException("Failed to update discount");
			}
		}
	}
}
