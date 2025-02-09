namespace Shop.Api.ViewModels.Auth;

public class LoginResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}