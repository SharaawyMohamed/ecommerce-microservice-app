using Basket.Application.Common.Modules;
using Basket.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Features.Basket.Commands.DeleteShoppingBasket
{
	public class DeleteShoppingBasketCommandHandler : IRequestHandler<DeleteShoppingBasketCommand, BaseResponse>
	{
		private readonly ILogger<DeleteShoppingBasketCommandHandler> _logger;
		private readonly IBasketRepository _repository;

		public DeleteShoppingBasketCommandHandler(IBasketRepository repository, ILogger<DeleteShoppingBasketCommandHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<BaseResponse> Handle(DeleteShoppingBasketCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.UserName))
				return BaseResponse.Failure("userName is required");

			await _repository.DeleteBasketAsync(request.UserName);
			_logger.LogInformation("Shopping basket deleted for user {UserName}", request.UserName);
			return BaseResponse.Success(null, "Basket deleted");
		}
	}
}
