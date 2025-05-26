using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.Edit;

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("ایمیل نامعتبر است.");

        RuleFor(x => x.Avatar)
            .JustImageFile();
    }
}