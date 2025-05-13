using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Users;

public class ChangePasswordViewModel
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    public string CurrentPassword { get; set; }
    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [MinLength(6, ErrorMessage = "{0} باید بیشتر از 6 کاراکتر باشد.")]
    public string Password { get; set; }    
    [Display(Name = "تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید.")]
    [MinLength(6, ErrorMessage = "{0} باید بیشتر از 6 کاراکتر باشد.")]
    [Compare(nameof(Password), ErrorMessage = "کلمات عبور جدید یکسان نیستند.")]
    public string ConfirmPassword { get; set; }

}