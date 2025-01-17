using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Orders.Checkout;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("نام"));

        RuleFor(x => x.Family)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("نام خانوادگی"));

        RuleFor(x => x.City)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("شهر"));

        RuleFor(x => x.Province)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("استان"));

        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("UserId"));

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("شماره موبایل"))
            .MaximumLength(11).WithMessage("شماره موبایل نامعتبر است.")
            .MinimumLength(11).WithMessage("شماره موبایل نامعتبر است.");

        RuleFor(x => x.NationalCode)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("کد ملی"))
            .MaximumLength(11).WithMessage("کد ملی نامعتبر است.")
            .MinimumLength(11).WithMessage("کد ملی نامعتبر است.")
            .ValidNationalId();

        RuleFor(x => x.PostalAddress)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("آدرس پستی"));

        RuleFor(x => x.PostalCode)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.required("کد پستی"));
        

    }
}