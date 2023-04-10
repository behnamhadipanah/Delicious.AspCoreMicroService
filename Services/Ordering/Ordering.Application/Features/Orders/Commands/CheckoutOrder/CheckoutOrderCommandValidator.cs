using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandValidator:AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        
        RuleFor(x=>x.Username).NotEmpty().WithMessage("{Username} is Required")
            .NotNull().MaximumLength(50).WithMessage("{Username} must not exceed 50 characters");

        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("{Email} is Required").NotNull();

        RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("{TotalPrice} is Required")
            .GreaterThan(0).WithMessage("{TotalPrice} should be greater than 0");



    }
}