using Common.Application;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Application.Users.Create;

public record CreateUserCommand(
    string Name,
    string Family,
    string PhoneNumber,
    string Email,
    string Password,
    Gender Gender
    ) : IBaseCommand;