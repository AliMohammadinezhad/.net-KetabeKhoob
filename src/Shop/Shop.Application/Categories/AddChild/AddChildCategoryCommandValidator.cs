using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.AddChild;

public class AddChildCategoryCommandValidator : AbstractValidator<AddChildCategoryCommand>
{
    public AddChildCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("اسلاگ"));
    }
}