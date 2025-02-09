using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Shop.Api.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "شماره تلفن را وارد کنید.")]
    [Length(11, 11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "کلمه عبور را وارد کنید.")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "تکرار کلمه عبور را وارد کنید.")]
    [MinLength(6, ErrorMessage = "تکرار کلمه عبور باید بیشتر از 5 کاراکتر باشد.")]
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نیستند.")]
    public required string ConfirmPassword { get; set; }
}