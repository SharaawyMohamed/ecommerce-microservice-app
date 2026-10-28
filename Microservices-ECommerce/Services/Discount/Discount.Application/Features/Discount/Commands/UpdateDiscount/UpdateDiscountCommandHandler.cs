using Discount.Application.Protos;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.UpdateDiscount
{
	public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, Coupon>
	{
		private readonly IDiscountRepository _discountRepository;

		public UpdateDiscountCommandHandler(IDiscountRepository discountRepository)
		{
			_discountRepository = discountRepository;
		}

		public async Task<Coupon> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
		{
			var coupon = await _discountRepository.GetDiscountAsync(request.ProductName);
			if (coupon is null || coupon.Id != request.Id)
			{
				throw new InvalidOperationException($"Discount with product name '{request.ProductName}' not found or Id mismatch.");
			}

			var updatedCoupon = new Coupon
			{
				ProductName = request.ProductName,
				Description = request.Description,
				Amount = request.Amount
			};

			var is_updated = await _discountRepository.UpdateDiscountAsync(updatedCoupon);
			return is_updated ? updatedCoupon : throw new InvalidOperationException("Failed to update discount");
		}
	}
}
