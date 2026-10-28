using Discount.Application.Common.Models;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount
{
	public class CreateCouponDiscountCommandHandler : IRequestHandler<CreateCouponDiscountCommand, Coupon?>
	{
		private readonly IDiscountRepository _discountRepository;
		public CreateCouponDiscountCommandHandler(IDiscountRepository discountRepository)
		{
			_discountRepository = discountRepository;
		}
		public async Task<Coupon?> Handle(CreateCouponDiscountCommand request, CancellationToken cancellationToken)
		{
			var discount = new Coupon
			{
				ProductName = request.ProductName,
				Description = request.Description,
				Amount = request.Amount
			};
			var coupon= await _discountRepository.CreateDiscountAsync(discount);
			return coupon is not null? coupon : throw new InvalidOperationException("Failed to create discount");
		}
	}
}
