using Common.Application;
using Common.Application.Validation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Application.Users.Edit;

public record EditUserCommand(
    long Id,
    string Name,
    string Family,
    string PhoneNumber,
    string Email,
    Gender Gender,
    IFormFile? Avatar
) : IBaseCommand
{
    public long UserId { get; set; }
}