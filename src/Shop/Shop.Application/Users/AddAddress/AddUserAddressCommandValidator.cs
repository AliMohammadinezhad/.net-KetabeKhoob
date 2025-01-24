using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Users.AddAddress;

public class AddUserAddressCommandValidator : AbstractValidator<AddUserAddressCommand>
{
    public AddUserAddressCommandValidator()
    {
        RuleFor(x => x.City)
            .NotNull().WithMessage(ValidationMessages.required("شهر"));

        RuleFor(x => x.Shire)
            .NotNull().WithMessage(ValidationMessages.required("استان"));

        RuleFor(x => x.Name)
            .NotNull().WithMessage(ValidationMessages.required("نام"));

        RuleFor(x => x.Family)
            .NotNull().WithMessage(ValidationMessages.required("نام خانوادگی"));

        RuleFor(x => x.NationalCode)
            .NotNull().WithMessage(ValidationMessages.required("کد ملی"));

        RuleFor(x => x.PostalAddress)
            .NotNull().WithMessage(ValidationMessages.required("آدرس پستی"));

        RuleFor(x => x.PostalCode)
            .Length(10)
            .NotNull().WithMessage(ValidationMessages.required("کد پستی"));
    }
}