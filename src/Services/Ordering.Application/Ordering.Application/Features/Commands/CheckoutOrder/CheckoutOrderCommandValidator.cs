using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator:AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(f => f.UserName)
                .NotEmpty().WithMessage("{UserName} can not be Empty!")
                .NotNull().WithMessage("{UserName} can not be Null!")
                .MaximumLength(50).WithMessage("UserName can not be more than 50");

            RuleFor(f => f.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} can not be Empty!");
        }
    }
}
