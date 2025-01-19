using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Roles.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

    }
}