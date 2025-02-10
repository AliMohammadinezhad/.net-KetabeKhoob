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
            Permissions = role.Permissions.Select(x => x.Permission).ToList(),
            Title = role.Title
        };
    }
}