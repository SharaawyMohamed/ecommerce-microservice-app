using Basket.Core.Repositories;
using Catalog.Application.Common.Models;
using MediatR;

namespace Basket.Application.Features.Basket.Commands.DeleteShoppingBasket
{
	public class DeleteShoppingBasketCommandHandler : IRequestHandler<DeleteShoppingBasketCommand, BaseResponse>
	{
		private readonly IBasketRepository _repository;

		public DeleteShoppingBasketCommandHandler(IBasketRepository repository)
		{
			_repository = repository;
		}

		public async Task<BaseResponse> Handle(DeleteShoppingBasketCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.UserName))
				return BaseResponse.Failure("userName is required");

			await _repository.DeleteBasketAsync(request.UserName);

			return BaseResponse.Success(null, "Basket deleted");
		}
	}
}
