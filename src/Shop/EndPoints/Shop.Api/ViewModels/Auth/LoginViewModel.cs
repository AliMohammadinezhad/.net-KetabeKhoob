using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Shop.Api.ViewModels.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "شماره تلفن را وارد کنید.")]
    [Length(11, 11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "کلمه عبور را وارد کنید.")]
    public required string Password { get; set; }
}