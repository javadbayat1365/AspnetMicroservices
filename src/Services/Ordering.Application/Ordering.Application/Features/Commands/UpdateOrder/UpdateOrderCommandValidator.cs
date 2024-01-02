using FluentValidation;

namespace Ordering.Application.Features.Commands.UpdateOrder;

public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(f => f.UserName)
            .NotEmpty().WithMessage("{UserName} can not be null")
            .NotNull().MaximumLength(100);

        RuleFor(f => f.Id).NotNull().NotEmpty();
    }
}
