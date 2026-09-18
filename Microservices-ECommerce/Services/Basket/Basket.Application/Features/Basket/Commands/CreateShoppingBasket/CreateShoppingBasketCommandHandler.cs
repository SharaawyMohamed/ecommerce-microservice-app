using Basket.Core.Repositories;
using Catalog.Application.Common.Models;
using MediatR;
using Mapster;
using Basket.Application.Features.Basket.Queries.GetShoppingBasket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Features.Basket.Commands.CreateShoppingBasket
{
	public class CreateShoppingBasketCommandHandler : IRequestHandler<CreateShoppingBasketCommand, BaseResponse>
	{
		private readonly IBasketRepository _repository;

		public CreateShoppingBasketCommandHandler(IBasketRepository repository)
		{
			_repository = repository;
		}

		public async Task<BaseResponse> Handle(CreateShoppingBasketCommand request, CancellationToken cancellationToken)
		{
			if (request?.Cart == null)
			{
				return BaseResponse.Failure("Invalid shopping cart payload");
			}

			await _repository.AddOrUpdateBasketAsync(request.Cart);

			// Map domain entity to application response DTO using Mapster
			var response = request.Cart.Adapt<GetShoppingBasketResponse>();

			return BaseResponse.Success(response, "Basket saved successfully");
		}
	}
}
