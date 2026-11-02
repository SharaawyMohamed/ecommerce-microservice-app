using FluentValidation;

namespace Ordering.Application.Features.Order.Commands.DeleteOrder
{
	public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
	{
		public DeleteOrderCommandValidator()
		{
			RuleFor(x => x.userName).NotEmpty().WithMessage("User Name is required");
		}
	}

}
