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
            Permissions = role.Permissions.MapRolePermissions(),
            Title = role.Title
        };
    }


    private static List<PermissionDto> MapRolePermissions(this List<RolePermission> rolePermission)
    {
        return rolePermission.Select(x => MapPermission(x.Permission)).ToList();
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