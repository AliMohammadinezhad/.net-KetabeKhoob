﻿using FluentValidation;

namespace Shop.Application.Orders.IncreaseItemCount;

public class IncreaseOrderItemCountCommandValidator : AbstractValidator<IncreaseOrderItemCountCommand>
{
    public IncreaseOrderItemCountCommandValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1)
            .WithMessage("تعداد باید بیشتر یا برابر 1 باشد.");
    }
}