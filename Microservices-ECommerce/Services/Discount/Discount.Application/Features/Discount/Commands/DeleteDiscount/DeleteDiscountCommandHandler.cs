using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.DeleteDiscount
{
	internal class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
	{
		private readonly IDiscountRepository _discountRepository;

		public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
		{
			_discountRepository = discountRepository;
		}

		public Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
		{
			var coupon = _discountRepository.GetDiscountAsync(request.productName).Result;
			if (coupon is null)
			{
				throw new InvalidOperationException($"Discount with product name '{request.productName}' not found.");
			}
			var is_deleted = _discountRepository.DeleteDiscountAsync(request.productName).Result;
			return is_deleted ? Task.FromResult(true) : throw new InvalidOperationException("Failed to delete discount");
		}
	}
}
