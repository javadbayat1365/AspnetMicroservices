using FluentValidation;

namespace Ordering.Application.Features.Commands.DeleteOrder;

public class DeleteOrderCommandHandlerValidator
    :AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandHandlerValidator()
    {
        RuleFor(f => f.Id).NotEmpty().NotNull().WithMessage("Id Can Not Be Null!");
    }
}
