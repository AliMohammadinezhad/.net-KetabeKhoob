using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Users.ChangePassword;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(6).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));


        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور جدید"))
            .MinimumLength(6).WithMessage(ValidationMessages.required("کلمه عبور جدید"));
    }
}