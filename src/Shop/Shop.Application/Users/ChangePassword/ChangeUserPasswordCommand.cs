using System.Security.Cryptography;
using Common.Application;

namespace Shop.Application.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string Password { get; set; }

    public ChangeUserPasswordCommand(long userId, string currentPassword, string password)
    {
        UserId = userId;
        CurrentPassword = currentPassword;
        Password = password;
    }
}