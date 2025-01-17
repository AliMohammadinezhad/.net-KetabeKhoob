﻿using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Roles.Edit;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

    }
}