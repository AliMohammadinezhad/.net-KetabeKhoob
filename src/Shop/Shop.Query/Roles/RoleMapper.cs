using System.ComponentModel;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles;

public static class RoleMapper
{
    public static RoleDto? Map(this Role? role)
    {
        if (role == null)
            return null;

        return new RoleDto
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(x => new RolePermissionDto()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Permission = MapPermission(x.Permission),
                RoleId = x.RoleId,
            }).ToList(),
            Title = role.Title
        };
    }

    private static PermissionDto MapPermission(Permission permission)
    {
        return permission switch
        {
            Permission.AdminPanel => PermissionDto.AdminPanel,
            Permission.EditProfile => PermissionDto.EditProfile,
            Permission.ChangePassword => PermissionDto.ChangePassword,
            _ => throw new InvalidEnumArgumentException()
        };
    }
}