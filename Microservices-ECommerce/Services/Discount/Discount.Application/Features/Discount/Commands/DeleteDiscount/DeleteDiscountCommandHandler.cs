using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Features.Discount.Commands.DeleteDiscount
{
	internal class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
	{
		private readonly IDiscountRepository _discountRepository;
		private readonly ILogger<DeleteDiscountCommandHandler> _logger;
		public DeleteDiscountCommandHandler(IDiscountRepository discountRepository, ILogger<DeleteDiscountCommandHandler> logger)
		{
			_discountRepository = discountRepository;
			_logger = logger;
		}

		public Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
		{
			var coupon = _discountRepository.GetDiscountAsync(request.productName).Result;
			if (coupon is null)
			{
				_logger.LogError("Discount with product name '{ProductName}' not found.", request.productName);
				throw new InvalidOperationException($"Discount with product name '{request.productName}' not found.");
			}
			var is_deleted = _discountRepository.DeleteDiscountAsync(request.productName).Result;
			if (is_deleted)
			{
				_logger.LogInformation("Discount deleted for product name '{ProductName}'", request.productName);
				return Task.FromResult(true);
			}
			else
			{
				_logger.LogError("Failed to delete discount for product name '{ProductName}'", request.productName);
				throw new InvalidOperationException("Failed to delete discount");
			}
		}
	}
}
