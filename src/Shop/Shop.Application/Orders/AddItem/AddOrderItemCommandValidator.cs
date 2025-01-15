using FluentValidation;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1)
            .WithMessage("تعداد باید بیشتر یا برابر 1 باشد.");
    }
}