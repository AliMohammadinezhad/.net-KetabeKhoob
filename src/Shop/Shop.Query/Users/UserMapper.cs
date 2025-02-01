using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Enums;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users;

public static class UserMapper
{
    public static UserDto Map(this User user)
    {
        return new UserDto
        {
            AvatarName = user.AvatarName,
            Email = user.Email,
            Family = user.Family,
            Password = user.Password,
            Gender = MapUserGender(user.Gender),
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            UserRoles = MapUserRoles(user.UserRoles)
        };
    }

    private static List<UserRoleDto> MapUserRoles(List<UserRole> userRoles)
    {
        return userRoles.Select(x => new UserRoleDto()
        {
            RoleId = x.RoleId,
            RoleTitle = ""
        }).ToList();
    }

    public static async Task<UserDto> SetUserRoles(this UserDto userDto, ShopContext context)
    {
        var roleIds = userDto.UserRoles.Select(x => x.RoleId);
        var result = await context.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();
        var roles = result.Select(role => new UserRoleDto{
            RoleId = role.Id,
            RoleTitle = role.Title,
            }).ToList();

        userDto.UserRoles = roles;

        return userDto;
    }

    public static UserFilterData MapFilterData(this User user)
    {
        return new UserFilterData()
        {
            Id = user.Id,
            AvatarName = user.AvatarName,
            CreationDate = user.CreationDate,
            Email = user.Email,
            Family = user.Family,
            Gender = MapUserGender(user.Gender),
            PhoneNumber = user.PhoneNumber,
            Name = user.Name,
        };
    }

    private static GenderDto MapUserGender(Gender gender)
    {
        return gender switch
        {
            Gender.Male => GenderDto.Male,
            Gender.Female => GenderDto.Female,
            Gender.None => GenderDto.None,
            _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
        };
    }
}