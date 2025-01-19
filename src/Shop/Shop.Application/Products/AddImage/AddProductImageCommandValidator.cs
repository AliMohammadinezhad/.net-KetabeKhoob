using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.RemoveImage.AddImage;

public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
{
    public AddProductImageCommandValidator()
    {
        RuleFor(x => x.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("عکس"))
            .JustImageFile();

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0);

    }
}